﻿using System;
namespace Aspyct.DependencyInjection.Sample
{
	// You can redefine dependencies in child configurations
	// Here, we will use a different implementation of IMessageService
	// while still offering the IGreeter
	[AutoProvider(
		typeof(IMessageService),
		typeof(HelloWorldMessageService)
	)]
	public class HelloWorldConfiguration : BaseConfiguration
	{
	}
}

