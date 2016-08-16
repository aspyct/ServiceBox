using System;

namespace ServiceBox
{
	public class ProviderAttribute : BuilderMakerAttribute
	{
		public ProviderAttribute(bool singleton = false) : base(singleton)
		{
		}
	}
}

