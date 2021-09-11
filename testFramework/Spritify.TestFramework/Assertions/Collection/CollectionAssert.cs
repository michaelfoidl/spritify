using System;
using System.Collections.Generic;
using System.Linq;
using Spritify.TestFramework.Assertions.Equality;

namespace Spritify.TestFramework.Assertions.Collection
{
    public static class CollectionAssert
    {
        public static void IsEmpty<TItem>(IEnumerable<TItem> actual)
        {
            EqualityAssert.AreEqual(0, actual.Count(), "Count");
        }

        public static void IsNotEmpty<TItem>(IEnumerable<TItem> actual)
        {
            EqualityAssert.AreNotEqual(0, actual.Count(), "Count");
        }

        public static void AreOfEqualLength<TExpectedItem, TActualItem>(IEnumerable<TExpectedItem> expected, IEnumerable<TActualItem> actual)
        {
            if (expected == null || actual == null)
            {
                NUnit.Framework.Assert.Fail("Expected collections to be of equal length but at least one of them was null.");
            }

            if (ReferenceEquals(expected, actual))
            {
                return;
            }

            AssertAreOfEqualLength(expected, actual);
        }

        public static void AreEqual<TItem>(IEnumerable<TItem> expected, IEnumerable<TItem> actual)
        {
            if (ReferenceEquals(expected, actual))
            {
                return;
            }

            AssertAreEqual(expected, actual);
        }


        public static void AreEqual<TItem>(IEnumerable<TItem> expected, IEnumerable<TItem> actual, IComparer<TItem> comparer)
        {
            if (ReferenceEquals(expected, actual))
            {
                return;
            }

            AssertAreEqual(expected, actual, comparer);
        }

        public static void AreEquivalent<TItem, TKey>(IEnumerable<TItem> expected, IEnumerable<TItem> actual, Func<TItem, TKey> orderFunction, IComparer<TItem> comparer)
        {
            if (ReferenceEquals(expected, actual))
            {
                return;
            }

            var expectedOrdered = expected.OrderBy(orderFunction).ToList();
            var actualOrdered = actual.OrderBy(orderFunction).ToList();

            AssertAreEqual(expectedOrdered, actualOrdered, comparer);
        }

        public static void Contains<TItem>(IEnumerable<TItem> actual, Predicate<TItem> predicate)
        {
            if (!actual.Any(item => predicate(item)))
            {
                NUnit.Framework.Assert.Fail("Collection does not contain an element that matches the predicate.");
            }
        }

        public static void Contains<TItem>(IEnumerable<TItem> actual, TItem expected)
        {
            if (!actual.Contains(expected))
            {
                NUnit.Framework.Assert.Fail("Collection does not contain the expected element.");
            }
        }

        public static void DoesNotContain<TItem>(IEnumerable<TItem> actual, Predicate<TItem> predicate)
        {
            if (actual.Any(item => predicate(item)))
            {
                NUnit.Framework.Assert.Fail("Collection contains an element that matches the predicate.");
            }
        }

        public static void DoesNotContain<TItem>(IEnumerable<TItem> actual, TItem unexpected)
        {
            if (actual.Contains(unexpected))
            {
                NUnit.Framework.Assert.Fail("Collection contains the unexpected element.");
            }
        }

        public static void IsOfLength<TItem>(IEnumerable<TItem> actual, int expectedLength)
        {
            EqualityAssert.AreEqual(expectedLength, actual.Count(), "Collection length");
        }

        private static void AssertAreOfEqualLength<TExpectedItem, TActualItem>(IEnumerable<TExpectedItem> expected, IEnumerable<TActualItem> actual)
        {
            EqualityAssert.AreEqual(expected.Count(), actual.Count(), "Count");
        }

        private static void AssertAreEqual<TItem>(IEnumerable<TItem> expected, IEnumerable<TItem> actual)
        {
            var expectedList = expected.ToList();
            var actualList = actual.ToList();

            AssertAreOfEqualLength(expectedList, actualList);

            for (var i = 0; i < actualList.Count; i++)
            {
                EqualityAssert.AreEqual(expectedList.ElementAt(i), actualList.ElementAt(i), $"Index {i}");
            }
        }

        private static void AssertAreEqual<TItem>(IEnumerable<TItem> expected, IEnumerable<TItem> actual, IComparer<TItem> comparer)
        {
            var expectedList = expected.ToList();
            var actualList = actual.ToList();

            AssertAreOfEqualLength(expectedList, actualList);

            for (var i = 0; i < actualList.Count; i++)
            {
                EqualityAssert.AreEqual(expectedList.ElementAt(i), actualList.ElementAt(i), comparer, $"Index {i}");
            }
        }
    }
}