using NUnit.Framework;
using System;
using System.Collections.Generic;

using Procinto.LxRedux;
using Procinto.Utility;

namespace Procinto.TestLxRedux
{
	[TestFixture()]
	public class TestAbceq
	{
		string abcAF;
		string abcAFtoo;
		string abc123;
		List<string> listAFtooAF;
		List<string> listAFAFtoo;

		void Setup ()
		{
			abcAF = "abcdef";
			abcAFtoo = "abcdef";
			abc123 = "123";
			listAFtooAF = new List<string> ()
				.And (abcAF).And (abcAFtoo);
			listAFAFtoo = new List<string> ()
				.And (abcAFtoo).And (abcAF);
		}

		[Test()]
		public void TestCtor ()
		{
			Setup ();

			Abceq q1 = new Abceq (listAFAFtoo);
			Abceq q2 = new Abceq (abcAF, abcAFtoo);

			Assert.AreNotEqual (q1, q2);
			// Not equal but functionally equal.
			Assert.AreEqual (1, q1.Index (0, 'b'));
			Assert.AreEqual (1, q2.Index (0, 'b'));

			// TODO more later if needed.
		}

		[Test]
		public void TestAbcAt ()
		{
			Setup ();
			Abceq qa1 = new Abceq (abcAF, abc123);

			Assert.AreEqual (abc123, qa1.AbcAt (1));
			Assert.AreEqual ("123", qa1.AbcAt (1));
		}

		[Test]
		public void TestCumulativeIndices ()
		{
			Abceq qa11a = new Abceq (abcAF, abc123, abc123, abcAF);
			Abceq qa = new Abceq (abcAF);
			Abceq q1a = new Abceq (abc123, abcAF);

			List<int> indices;
			long cumulative;

			// a; first
			indices = new List<int> ().And (0);
			cumulative = qa.CumulativeIndex (indices);
			Assert.AreEqual (0, cumulative);

			// f; last
			indices = new List<int> ().And (5);
			cumulative = qa.CumulativeIndex (indices);
			Assert.AreEqual (5, cumulative);

			// 1a; first carryover
			indices = new List<int> ().And (1).And (0);
			cumulative = q1a.CumulativeIndex (indices);
			Assert.AreEqual (0 + 1 * 6, cumulative);


			// b21c
			indices = new List<int> ()
				.And (1).And (1).And (0).And (2);
			cumulative = qa11a.CumulativeIndex (indices);
			Assert.AreEqual (2 + 0 * 6 + 1 * 6 * 3 + 1 * 6 * 3 * 3, cumulative);

		}

		[Test]
		public void TestCalculateIndividualIndices ()
		{
			// TODO HERE
		}
	}
}

