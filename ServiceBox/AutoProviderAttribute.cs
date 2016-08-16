using System;
namespace ServiceBox
{
	public class AutoProviderAttribute : Attribute
	{
		readonly bool _singleton;
		readonly Type _iface;
		readonly Type _cls;

		public AutoProviderAttribute(Type iface, Type cls, bool singleton = false)
		{
			_iface = iface;
			_cls = cls;
			_singleton = singleton;
		}
	}
}

