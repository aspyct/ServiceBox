using System;
namespace ServiceBox
{
	public class AutoProviderAttribute : BuilderMakerAttribute
	{
		Type _iface;
		Type _cls;

		public AutoProviderAttribute(Type iface, Type cls, bool singleton = false) : base(singleton)
		{
			_iface = iface;
			_cls = cls;
		}
	}
}

