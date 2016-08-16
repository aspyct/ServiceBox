using System;
namespace ServiceBox.Sample
{
	public class DefaultMessageService : IMessageService
	{
		public string GetMessage()
		{
			return "This is a boring default message...";
		}
	}
}

