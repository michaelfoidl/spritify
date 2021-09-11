namespace Spritify.TestFramework.Assertions.Nullable
{
    public class NullableAssert
    {
        public static void IsNull(object actual)
        {
            NUnit.Framework.Assert.Null(actual);
        }

        public static void IsNull(object actual, string propertyName)
        {
            NUnit.Framework.Assert.Null(actual, $"'{propertyName}' does not match.");
        }

        public static void IsNotNull(object actual)
        {
            NUnit.Framework.Assert.NotNull(actual);
        }

        public static void IsNotNull(object actual, string propertyName)
        {
            NUnit.Framework.Assert.NotNull(actual, $"'{propertyName}' does not match.");
        }
    }
}
