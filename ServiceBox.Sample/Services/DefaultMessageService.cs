using System;
namespace Aspyct.DependencyInjection.Sample
{
	public class DefaultMessageService : IMessageService
	{
		public string GetMessage()
		{
			return "This is a boring default message...";
		}
	}
}

