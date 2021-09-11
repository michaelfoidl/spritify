using System;
using Spritify.TestFramework.Assertions.Equality;
using Spritify.TestFramework.Assertions.Exception;
using Spritify.TestFramework.Assertions.Inequality;
using Spritify.TestFramework.Assertions.Nullable;
using Spritify.TestFramework.Assertions.Truth;

namespace Spritify.TestFramework.Assertions
{
    public static class Assert
    {
        public static void AreEqual(object expected, object actual)
        {
            EqualityAssert.AreEqual(expected, actual);
        }

        public static void AreNotEqual(object expected, object actual)
        {
            EqualityAssert.AreNotEqual(expected, actual);
        }

        public static void AreSame(object expected, object actual)
        {
            EqualityAssert.AreSame(expected, actual);
        }

        public static void AreNotSame(object expected, object actual)
        {
            EqualityAssert.AreNotSame(expected, actual);
        }

        public static void IsNotNull(object actual)
        {
            NullableAssert.IsNotNull(actual);
        }

        public static void IsNull(object actual)
        {
            NullableAssert.IsNull(actual);
        }

        public static void IsTrue(bool actual)
        {
            TruthAssert.IsTrue(actual);
        }

        public static void IsFalse(bool actual)
        {
            TruthAssert.IsFalse(actual);
        }

        public static void IsLess(IComparable expected, IComparable actual)
        {
            InequalityAssert.IsLess(expected, actual);
        }

        public static void IsLessOrEqual(IComparable expected, IComparable actual)
        {
            InequalityAssert.IsLessOrEqual(expected, actual);
        }

        public static void IsGreater(IComparable expected, IComparable actual)
        {
            InequalityAssert.IsGreater(expected, actual);
        }

        public static void IsGreaterOrEqual(IComparable expected, IComparable actual)
        {
            InequalityAssert.IsGreaterOrEqual(expected, actual);
        }

        public static void Throws(Action action)
        {
            ExceptionAssert.Throws(action);
        }

        public static void Throws<TException>(Action action) where TException : System.Exception
        {
            ExceptionAssert.Throws<TException>(action);
        }
    }
}
