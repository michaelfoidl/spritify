using JetBrains.Annotations;

namespace Spritify.TestFramework.Assertions.Truth
{
    public class TruthAssert
    {
        [ContractAnnotation("false => halt")]
        public static void IsTrue(bool actual)
        {
            NUnit.Framework.Assert.IsTrue(actual);
        }

        [ContractAnnotation("actual:false => halt")]
        public static void IsTrue(bool actual, string propertyName)
        {
            NUnit.Framework.Assert.IsTrue(actual, $"'{propertyName}' does not match.");
        }

        [ContractAnnotation("true => halt")]
        public static void IsFalse(bool actual)
        {
            NUnit.Framework.Assert.IsFalse(actual);
        }

        [ContractAnnotation("actual:true => halt")]
        public static void IsFalse(bool actual, string propertyName)
        {
            NUnit.Framework.Assert.IsFalse(actual, $"'{propertyName}' does not match.");
        }
    }
}
