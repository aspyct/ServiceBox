using System;
using System.Reflection;

namespace Aspyct.ServiceBox
{
	public class ProviderAttribute : Attribute
	{
		readonly bool _singleton;

		public ProviderAttribute(bool singleton = false)
		{
			_singleton = singleton;
		}

		public Builder MakeBuilder(Box box, MethodInfo method)
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
			public Box Box;
			public MethodInfo Method;

			public virtual object Build()
			{
				return Method.Invoke(Box, null);
			}
		}
	}
}

