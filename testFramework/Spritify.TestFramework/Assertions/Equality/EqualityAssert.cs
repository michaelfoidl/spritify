using System.Collections.Generic;
using Spritify.TestFramework.Assertions.Truth;

namespace Spritify.TestFramework.Assertions.Equality
{
    public class EqualityAssert
    {
        public static void AreEqual(object expected, object actual)
        {
            NUnit.Framework.Assert.AreEqual(expected, actual);
        }

        public static void AreEqual(object expected, object actual, string propertyName)
        {
            NUnit.Framework.Assert.AreEqual(expected, actual, $"'{propertyName}' does not match.");
        }

        public static void AreEqual<T>(T expected, T actual, IComparer<T> comparer)
        {
            var areEqual = comparer.Compare(expected, actual) == 0;

            TruthAssert.IsTrue(areEqual);
        }

        public static void AreEqual<T>(T expected, T actual, IComparer<T> comparer, string propertyName)
        {
            var areEqual = comparer.Compare(expected, actual) == 0;

            TruthAssert.IsTrue(areEqual, propertyName);
        }

        public static void AreNotEqual(object expected, object actual)
        {
            NUnit.Framework.Assert.AreNotEqual(expected, actual);
        }
        
        public static void AreNotEqual(object expected, object actual, string propertyName)
        {
            NUnit.Framework.Assert.AreNotEqual(expected, actual, $"'{propertyName}' does not match.");
        }

        public static void AreSame(object expected, object actual)
        {
            NUnit.Framework.Assert.AreSame(expected, actual);
        }

        public static void AreSame(object expected, object actual, string propertyName)
        {
            NUnit.Framework.Assert.AreSame(expected, actual, $"'{propertyName}' does not match.");
        }

        public static void AreNotSame(object expected, object actual)
        {
            NUnit.Framework.Assert.AreNotSame(expected, actual);
        }

        public static void AreNotSame(object expected, object actual, string propertyName)
        {
            NUnit.Framework.Assert.AreNotSame(expected, actual, $"'{propertyName}' does not match.");
        }
    }
}
