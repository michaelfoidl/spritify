namespace Spritify.TestFramework.Assertions.Type
{
    public class TypeAssert
    {
        public static void Is<T>(object value)
        {
            NUnit.Framework.Assert.IsInstanceOf<T>(value);
        }

        public static void IsNot<T>(object value)
        {
            NUnit.Framework.Assert.IsNotInstanceOf<T>(value);
        }
    }
}
