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
		/// <summary>
		/// The list of alphabets defines the space.
		/// </summary>
		Abceq Alphabets{ get; set; }

		/// <summary>
		/// The list of indices defines the point.
		/// </summary>
		List<int> Indices { get; set; }
		#endregion

		#region ctor/Create
		public LxPoint (Abceq sequenceOfAbcs)
		{
			Alphabets = sequenceOfAbcs;
			Indices = new List<int> ();
		}

		/// <summary>
		/// Sets the point in its alphabets space.
		/// </summary>
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

		/// <summary>
		/// Resets the indices.
		/// Also resets the alphabets.
		/// TODO: maybe keep it in its alphabets space?
		/// Rely on Abceq.IsCompatible (not yet implemented).
		/// TODO separate assignment function.
		/// </summary>
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

		/// <summary>
		/// Sets the point in its alphabets space.
		/// </summary>
		public LxPoint SetFrom (long numericValue)
		{
			this.Indices = this.Alphabets.CalculateIndividualIndices(numericValue);
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

