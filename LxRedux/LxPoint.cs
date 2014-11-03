using System;
using System.Collections.Generic;
using System.Text;

namespace Procinto.LxRedux
{
	/// <summary>
	/// Lx point.
	/// </summary>
	public class LxPoint
	{
		#region Data
		Abceq Alphabets{ get; set; }

		List<int> Indices { get; set; }
		#endregion

		#region ctor/Create
		public LxPoint (Abceq sequenceOfAbcs)
		{
			Alphabets = sequenceOfAbcs;
			Indices = new List<int> ();
		}

		public LxPoint SetFrom (string textValue)
		{
			if (null == textValue) {
				// TODO error
				return this;
			}
			int position = 0;
			foreach (var ch in textValue) {
				int index = Alphabets.Index (position, ch);
				Indices [position] = index;
				position++;
			}
			return this;
		}

		public LxPoint SetFrom (LxPoint p)
		{
			if (null == p) {
				// TODO error
				return this;
			}
			Alphabets = p.Alphabets;
			Indices = p.Indices;
			return this;
		}
		#endregion

		#region Value
		public string TextValue {
			// TODO - optimize
			get { 
				StringBuilder sb = new StringBuilder ();

				for (int position = 0; position < this.Indices.Count; position ++) {
					var abc = this.Alphabets.AbcAt (position);
					char ch = abc [this.Indices [position]];
					sb.Append (ch);
				}
			
				return sb.ToString ();
			}
		}

		public long NumericValue { 
			// TODO - optimize
			get {
				return this.Alphabets.CumulativeIndex (this.Indices);
			}
		}
		#endregion

	}
}

