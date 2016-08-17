using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Aspyct.DependencyInjection
{
	public abstract class ServiceBox
	{
		readonly Dictionary<Type, Builder> _builders;

		protected ServiceBox()
		{
			_builders = new Dictionary<Type, Builder>();

			MapBuilders();
		}

		protected T CreateAndInject<T>()
		{
			var instance = Activator.CreateInstance<T>();

			Inject(instance);

			return instance;
		}

		public void Inject(object target)
		{
			target.GetType()
		      	.GetTypeInfo()
				.DeclaredProperties
				.Where(property => property.GetCustomAttribute<InjectedAttribute>() != null)
			    .ForEach((property) => {
					var type = property.PropertyType;
					var dependency = Resolve(type);

					property.SetValue(target, dependency);
				});
		}

		public T Get<T>() where T : class
		{
			return Resolve(typeof(T)) as T;
		}

		object Resolve(Type type)
		{
			object dependency = null;

			Builder builder;
			if (_builders.TryGetValue(type, out builder)) {
				dependency = builder.Build();
			}
			else {
				throw new InvalidOperationException("Nothing provides " + type);
			}

			return dependency;
		}

		void MapBuilders()
		{
			// Traverse all levels of the service box
			List<TypeInfo> levels = new List<TypeInfo>();

			Type currentLevel = GetType();
			while (currentLevel != null) {
				var typeInfo = currentLevel.GetTypeInfo();

				levels.Add(typeInfo);

				currentLevel = typeInfo.BaseType;
			}

			// In reverse order, so as to take children's providers first
			levels.Reverse<TypeInfo>().ForEach((typeInfo) => {
				MapAutoProviders(typeInfo);
				MapMethodProviders(typeInfo);
			});
		}

		void MapAutoProviders(TypeInfo typeInfo)
		{
			var customAttributes = typeInfo.GetCustomAttributes<AutoProviderAttribute>(
				false // take only from this class level, not from base and derived classes
			);

			Debug.WriteLine("Mapping auto providers for " + typeInfo);
			Debug.WriteLine("{0} attributes found", customAttributes.Count());
				
			customAttributes
				.ForEach((attr) => {
					Debug.WriteLine("Mapping {0} from {1}", attr.InterfaceType, typeInfo);
					var builder = attr.MakeBuilder(this);
					AddBuilder(attr.InterfaceType, builder);
				});
		}

		void MapMethodProviders(TypeInfo typeInfo)
		{
			typeInfo
				.DeclaredMethods
				.ForEach((method) => {
					var attr = method.GetCustomAttribute<ProviderAttribute>();

					if (attr != null) {
						var builder = attr.MakeBuilder(this, method);
						AddBuilder(method.ReturnType, builder);
					}
				});
		}

		void AddBuilder(Type interfaceType, Builder builder)
		{
			_builders[interfaceType] = builder;
		}
	}
}

