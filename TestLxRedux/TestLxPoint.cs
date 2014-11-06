using NUnit.Framework;
using System;

using Procinto;
using Procinto.LxRedux;

namespace Procinto.TestLxRedux
{
	[TestFixture()]
	public class TestLxPoint
	{
		[Test()]
		public void TestPoints ()
		{
			LxPoint p;

			// Single alphabet.
			p = new LxPoint (new Abceq ("abcdef"));
			Assert.IsNotNull (p);
			Assert.IsFalse (p.HasBeenSet);

			p.SetFrom ("f");
			Assert.IsTrue (p.HasBeenSet);
			Assert.AreEqual ("f", p.TextValue);
			Assert.AreEqual (5, p.NumericValue);

			p.SetFrom (0);
			Assert.IsTrue (p.HasBeenSet);
			Assert.AreEqual ("a", p.TextValue);
			Assert.AreEqual (0, p.NumericValue);

			try {
				p.SetFrom (-1);
				Assert.Fail ("When setting an LxPoint from -1, TestPoints did not throw an LxException");
			} catch (LxException) {
				// Correctly threw an LxException.
			} catch (Exception ex) {
				Assert.Fail ("When setting an LxPoint from -1, TestPoints threw an exception other than LxException: " + ex);
			}

			try {
				p.SetFrom (6);
				Assert.Fail ("When setting an LxPoint from out of bounds, TestPoints did not throw an LxException");
			} catch (LxException) {
				// Correctly threw an LxException.
			} catch (Exception ex) {
				Assert.Fail ("When setting an LxPoint from out of bounds, TestPoints threw an exception other than LxException: " + ex);
			}

			try {
				p.SetFrom ("x");
				Assert.Fail ("When setting an LxPoint from out of bounds, TestPoints did not throw an LxException");
			} catch (LxException) {
				// Correctly threw an LxException.
			} catch (Exception ex) {
				Assert.Fail ("When setting an LxPoint from out of bounds, TestPoints threw an exception other than LxException: " + ex);
			}

			// Trivial alphabet.
			p = new LxPoint (new Abceq ("_"));
			Assert.IsNotNull (p);
			Assert.IsFalse (p.HasBeenSet);

			p.SetFrom (0);
			Assert.IsTrue (p.HasBeenSet);
			Assert.AreEqual (0, p.NumericValue);
			Assert.AreEqual ("_", p.TextValue);

			// Sort of normal alphabet.
			p = new LxPoint (new Abceq ("abcdef", "123", "123", "abcdef"));
			Assert.IsNotNull (p);
			Assert.IsFalse (p.HasBeenSet);

			p.SetFrom ("b31f");
			Assert.IsTrue (p.HasBeenSet);
			Assert.AreEqual ("b31f", p.TextValue);
			Assert.AreEqual (5 + 0 * 6 + 2 * 6 * 3 + 1 * 6 * 3 * 3, p.NumericValue);

			p.SetFrom (0);
			Assert.IsTrue (p.HasBeenSet);
			Assert.AreEqual ("a11a", p.TextValue);
			Assert.AreEqual (0, p.NumericValue);

			// Max from string
			p.SetFrom ("f33f");
			Assert.AreEqual ("f33f", p.TextValue);

			// Max from number
			p.SetFrom (5 + 2 * 6 + 2 * 6 * 3 + 5 * 6 * 3 * 3);
			Assert.IsNotNull (p);
			Assert.AreEqual ("f33f", p.TextValue);

			// Out of bounds
			try {
				p.SetFrom (5 + 2 * 6 + 2 * 6 * 3 + 5 * 6 * 3 * 3 + 1);
				Assert.Fail ("When setting an LxPoint out of bounds, TestPoints did not throw an LxException");
			} catch (LxException) {
				// Correctly threw an LxException.
			} catch (Exception ex) {
				Assert.Fail ("When setting an LxPoint out of bounds, TestPoints threw an exception other than LxException: " + ex);
			}

		}
	}
}

