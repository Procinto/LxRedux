using System;
using System.Collections.Generic;

namespace Procinto.LxRedux
{
	public class Abceq
	{
		#region Data
		List<string> Abc{ get; set; }

		/// <summary>
		/// Reverse indexing.
		/// At each position, ch => index.
		/// Zero based.
		/// </summary>
		List<Dictionary<char, int>> Indices{ get; set; }

		List<long> Weights = null;
		#endregion

		#region Weights
		void CalculateWeights ()
		{
			Weights = new List<long> (Abc.Count);
			if (Abc.Count < 1) {
				return;
			}

			foreach (var _ in Abc) {
				Weights.Add (0L);
			}

			int index = Weights.Count - 1;
			Weights [index] = 1;
			for (index--; index >= 0; index--) {
				Weights [index] = Weights [index + 1] * Abc [index + 1].Length;
			}
		}

		long Weight (int position)
		{
			if (position < 0 || position >= Abc.Count) {
				// TODO error
				return 0L;
			}
			if (null == Weights) {
				CalculateWeights ();
			}
			return Weights [position];
		}
		#endregion

		#region Indices
		void CalculateIndices ()
		{
			Indices = new List<Dictionary<char, int>> (Abc.Count);
			if (Abc.Count < 1) {
				return;
			}

			for (int pos = 0; pos < Abc.Count; pos++) {
				var theAbc = Abc [pos];
				Indices.Add (new Dictionary<char, int> (theAbc.Length));

				int index = 0;
				foreach (var ch in theAbc.ToCharArray()) {
					Indices [pos] [ch] = index++;
				}
			}

		}

		/// <summary>
		/// Where the given character is at,
		/// in the given position.
		/// </summary>
		public int Index (int pos, char ch)
		{
			if (pos < 0 || pos >= Abc.Count) {
				throw new LxException ("Index is out of bounds on pos=" + pos + ", max=" + (Abc.Count - 1));
			}
			if (null == Indices) {
				CalculateIndices ();
			}
			if (pos > Indices.Count) {
				throw new LxException ("Index is out of bounds on pos=" + pos + ", should be less than " + Indices.Count);
			}
			var theIndex = Indices [pos];
			if (!theIndex.ContainsKey (ch)) {
				throw new LxException ("Index is out of bounds: at pos=" + pos + ", ch='" + ch + "' is not defined");
			}
			return theIndex [ch];

		}

		/// <summary>
		/// Based on the values of individual indices of characters in each alphabet,
		/// calculate the index of the point in the space defined by the sequence of alphabets.
		/// </summary>
		public long CumulativeIndex (IEnumerable<int>charIndices)
		{
			long retval = 0;

			if (null == charIndices) {
				throw new LxException ("CumulativeIndex cannot be calculated on null indices");
			}

			int position = 0;
			foreach (var index in charIndices) {
				retval += Weight (position) * index;
				position++;
			}
			return retval;
		}

		public List<int> CalculateIndividualIndices (long cumulativeIndex)
		{
			if (cumulativeIndex < 0L) {
				throw new LxException ("CalculateIndividualIndices: invalid cumulativeIndex=" + cumulativeIndex);
			}

			List<int> retval = new List<int> ();
			long remainder = cumulativeIndex;

			// major to minor
			for (int pos = 0; pos < Abc.Count; pos++) {
				long w = Weight (pos);
				int index = (int)(remainder / w); // long integer division
				if (index >= Abc [pos].Length) {
					throw new LxException ("CalculateIndividualIndices: at position=" + pos 
						+ " calculated index=" + index + " is beyond the alphabet size=" + Abc [pos].Length
					);
				}
				remainder -= w * index;
				retval.Add (index);
			}
			if (0 != remainder) {
				// todo error
			}

			return retval;
		}

		#endregion

		#region Ctor
		public Abceq (List<string> abc)
		{
			this.Abc = abc ?? new List<string> ();
		}

		public Abceq (params string[] abc)
		{
			this.Abc = new List<string> (abc);
		}

		public Abceq (Abceq a)
		{
			this.Abc = a.Abc;
		}
		#endregion

		#region Alphabets
		/// <summary>
		/// The alphabet at the given position.
		/// </summary>
		public string AbcAt (int position)
		{
			if (position < 0 || position >= Abc.Count) {
				// TODO error
				return null; 
			}

			return Abc [position];
		}
		#endregion

		// TODO compatible abceq

	}
}

