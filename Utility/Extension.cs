using System;
using System.Collections.Generic;

namespace Procinto.Utility
{
	public static class Extension
	{
		public static List<T> And<T> (this List<T> myself, T element)
		{
			myself.Add(element);
			return myself;
		}
	
	}
}

