using System;
namespace Aspyct.DependencyInjection.Sample
{
	public class HelloWorldMessageService : IMessageService
	{
		public string GetMessage()
		{
			return "Hello world!";
		}
	}
}

