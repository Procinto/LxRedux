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
			// TODO HERE
		}
	}
}

