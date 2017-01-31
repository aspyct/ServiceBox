using System;
using System.Reflection;

namespace Aspyct.DependencyInjection
{
	public class ProviderAttribute : Attribute
	{
		readonly bool _singleton;

		public ProviderAttribute(bool singleton = false)
		{
			_singleton = singleton;
		}

		public Builder MakeBuilder(ServiceBox box, MethodInfo method)
		{
			Builder builder = new FactoryBuilder {
				Box = box,
				Method = method
			};

			if (_singleton) {
				builder = new SingletonBuilderDecorator(builder);
			}

			return builder;
		}

		class FactoryBuilder : Builder
		{
			public ServiceBox Box;
			public MethodInfo Method;

			public virtual object Build()
			{
				return Method.Invoke(Box, null);
			}
		}
	}
}

