using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ServiceBox
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

		protected void Inject(object target)
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
			Type currentLevel = GetType();

			while (currentLevel != null) {
				var typeInfo = currentLevel.GetTypeInfo();

				typeInfo
					.DeclaredMethods
					.ForEach((method) => {
						var attr = method.GetCustomAttribute<ProviderAttribute>();

						if (attr != null) {
							var builder = attr.MakeBuilder(this, method);
							_builders[method.ReturnType] = builder;
						}
					});


				currentLevel = typeInfo.BaseType;
			}
		}
	}
}

