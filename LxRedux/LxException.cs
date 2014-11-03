using System;

namespace Procinto.LxRedux
{
	public class LxException : Exception
	{
		public LxException() { }
		public LxException(string message) : base(message) { }
		public LxException(string message, Exception inner) : base(message, inner) { }
	}
}

