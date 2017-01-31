using System;
namespace Aspyct.DependencyInjection
{
	public class SingletonBuilderDecorator : Builder
	{
		Builder _innerBuilder;
		object _singleton;

		public SingletonBuilderDecorator(Builder innerBuilder)
		{
			_innerBuilder = innerBuilder;
		}

		public object Build()
		{
			if (_singleton == null) {
				lock (this) {
					if (_singleton == null) {
						_singleton = _innerBuilder.Build();
					}
				}
			}

			return _singleton;
		}
	}
}

