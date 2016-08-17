using System;
namespace Aspyct.ServiceBox.Sample
{
	public class HelloWorldMessageService : IMessageService
	{
		public string GetMessage()
		{
			return "Hello world!";
		}
	}
}

