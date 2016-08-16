using System;
using System.Reflection;

namespace ServiceBox
{
	public class ProviderAttribute : Attribute
	{
		bool _singleton;

		public ProviderAttribute(bool singleton=false)
		{
			_singleton = singleton;
		}

		public Builder MakeBuilder(ServiceBox box, MethodInfo method)
		{
			if (_singleton) {
				return new SingletonBuilder {
					Box = box,
					Method = method
				};
			}
			else {
				return new FactoryBuilder {
					Box = box,
					Method = method
				};
			}
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

		class SingletonBuilder : FactoryBuilder
		{
			volatile object _singleton;

			public override object Build()
			{
				if (_singleton == null) {
					lock(this) {
						if (_singleton == null) {
							_singleton = base.Build();
						}
					}
				}

				return _singleton;
			}
		}
	}
}

