﻿using System;
namespace Aspyct.ServiceBox.Sample
{
	public class Greeter : IGreeter
	{
		[Injected]
		public IMessageService MessageService { get; set; }

		public void Greet()
		{
			Console.WriteLine(MessageService.GetMessage());
		}
	}
}

