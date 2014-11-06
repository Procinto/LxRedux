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
		Abceq Alphabets { get; set; }

		/// <summary>
		/// The list of indices defines the point.
		/// </summary>
		List<int> Indices { get; set; }
		#endregion

		#region ctor/Create
		/// <summary>
		/// Need to provide the space.
		/// </summary>
		public LxPoint (Abceq sequenceOfAbcs)
		{
			Alphabets = sequenceOfAbcs;
			ResetIndices ();
		}

		/// <summary>
		/// Sets the point in its alphabets space.
		/// </summary>
		public LxPoint SetFrom (string textValue)
		{
			ResetIndices ();

			if (string.IsNullOrEmpty (textValue)) {
				throw new LxException ("SetFrom: cannot set from a null or empty string");
			}

			int position = 0;
			foreach (var ch in textValue) {
				int index = Alphabets.Index (position, ch);
				Indices.Add (index);
				position++;
			}
			return this;
		}

		/// <summary>
		/// Sets the point in its alphabets space.
		/// </summary>
		public LxPoint SetFrom (long numericValue)
		{
			ResetIndices ();
			this.Indices = this.Alphabets.CalculateIndividualIndices (numericValue);
			return this;
		}

		void ResetIndices ()
		{
			this.Indices = new List<int> ();
		}

#endregion

		#region Value
		public bool HasBeenSet {
			get { return null != Indices && Indices.Count > 0; }
		}

		public string TextValue {
			// TODO - optimize
			get {
				if (!HasBeenSet) {
					return string.Empty;
				}

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
				if (!HasBeenSet) {
					return -1;
				}

				return this.Alphabets.CumulativeIndex (this.Indices);
			}
		}
		#endregion

	}
}

