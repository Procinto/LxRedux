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

			int index = Weights.Count - 1;
			Weights [index] = 1;
			for (index--; index >= 0; index--) {
				Weights [index] = Weights [index + 1] * Abc [index + 1].Length;
			}
		}

		long Weight (int index)
		{
			if (index < 0 || index >= Abc.Count) {
				// TODO error
				return 0L;
			}
			if (null == Weights) {
				CalculateWeights ();
			}
			return Weights [index];
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
				Indices.Add (new Dictionary<char, int> (Abc [pos].Length));

				int index = 0;
				foreach (var ch in Abc[index]) {
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
			if (pos < 0 || pos > Abc.Count) {
				// TODO error
				return 0;
			}
			if (null == Indices) {
				CalculateIndices ();
			}
			return Indices [pos] [ch];

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
				if (position >= this.Weights.Count) {
					throw new LxException ("CumulativeIndex too many indices, more than " + this.Weights.Count);
				}
				retval += Weight (position) * index;
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


	}
}

