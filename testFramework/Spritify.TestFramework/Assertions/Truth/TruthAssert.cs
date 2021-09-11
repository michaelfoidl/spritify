namespace Spritify.TestFramework.Assertions.Truth
{
    public class TruthAssert
    {
        public static void IsTrue(bool actual)
        {
            NUnit.Framework.Assert.IsTrue(actual);
        }

        public static void IsTrue(bool actual, string propertyName)
        {
            NUnit.Framework.Assert.IsTrue(actual, $"'{propertyName}' does not match.");
        }

        public static void IsFalse(bool actual)
        {
            NUnit.Framework.Assert.IsFalse(actual);
        }

        public static void IsFalse(bool actual, string propertyName)
        {
            NUnit.Framework.Assert.IsFalse(actual, $"'{propertyName}' does not match.");
        }
    }
}
