using System;

namespace Spritify.TestFramework.Assertions.Inequality
{
    public class InequalityAssert
    {
        public static void IsLess(IComparable expected, IComparable actual)
        {
            NUnit.Framework.Assert.Less(expected, actual);
        }

        public static void IsLess(IComparable expected, IComparable actual, string propertyName)
        {
            NUnit.Framework.Assert.Less(expected, actual, $"'{propertyName}' does not match.");
        }

        public static void IsLessOrEqual(IComparable expected, IComparable actual)
        {
            NUnit.Framework.Assert.LessOrEqual(expected, actual);
        }

        public static void IsLessOrEqual(IComparable expected, IComparable actual, string propertyName)
        {
            NUnit.Framework.Assert.LessOrEqual(expected, actual, $"'{propertyName}' does not match.");
        }

        public static void IsGreater(IComparable expected, IComparable actual)
        {
            NUnit.Framework.Assert.Greater(expected, actual);
        }

        public static void IsGreater(IComparable expected, IComparable actual, string propertyName)
        {
            NUnit.Framework.Assert.Greater(expected, actual, $"'{propertyName}' does not match.");
        }

        public static void IsGreaterOrEqual(IComparable expected, IComparable actual)
        {
            NUnit.Framework.Assert.GreaterOrEqual(expected, actual);
        }

        public static void IsGreaterOrEqual(IComparable expected, IComparable actual, string propertyName)
        {
            NUnit.Framework.Assert.GreaterOrEqual(expected, actual, $"'{propertyName}' does not match.");
        }

    }
}
