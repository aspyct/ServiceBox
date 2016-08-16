using System;
namespace ServiceBox
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
	public class AutoProviderAttribute : Attribute
	{
		public Type InterfaceType { get; private set; }
		public Type ImplementationType { get; private set; }

		readonly bool _singleton;

		public AutoProviderAttribute(Type iface, Type impl, bool singleton = false)
		{
			InterfaceType = iface;
			ImplementationType = impl;
			_singleton = singleton;
		}

		public Builder MakeBuilder(ServiceBox box)
		{
			Builder builder = new AutoBuilder {
				Box = box,
				Cls = ImplementationType
			};

			if (_singleton) {
				builder = new SingletonBuilderDecorator(builder);
			}

			return builder;
		}

		class AutoBuilder : Builder
		{
			public Type Cls;
			public ServiceBox Box;

			public object Build()
			{
				var dependency = Activator.CreateInstance(Cls);
				Box.Inject(dependency);

				return dependency;
			}
		}
	}
}

