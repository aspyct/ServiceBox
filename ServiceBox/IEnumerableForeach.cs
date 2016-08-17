using System;
using System.Collections.Generic;

namespace Aspyct.ServiceBox
{
	public static class IEnumerableForeach
	{
		public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
		{
			foreach (var item in enumerable) {
				action(item);
			}
		}
	}
}

