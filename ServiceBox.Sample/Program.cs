using System;

namespace Aspyct.DependencyInjection.Sample
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			var configuration = new HelloWorldConfiguration();

			// Alternatively, use the BaseConfiguration
			// var configuration = new BaseConfiguration();

			// Get your entry point
			var greeter = configuration.Get<IGreeter>();

			greeter.Greet();
		}
	}
}
