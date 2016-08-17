using System;

namespace Aspyct.ServiceBox.Sample
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
