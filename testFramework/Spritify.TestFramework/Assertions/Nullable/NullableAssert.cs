using JetBrains.Annotations;

namespace Spritify.TestFramework.Assertions.Nullable
{
    public class NullableAssert
    {
        [ContractAnnotation("notnull => halt")]
        public static void IsNull(object actual)
        {
            NUnit.Framework.Assert.Null(actual);
        }

        [ContractAnnotation("actual:notnull => halt")]
        public static void IsNull(object actual, string propertyName)
        {
            NUnit.Framework.Assert.Null(actual, $"'{propertyName}' does not match.");
        }

        [ContractAnnotation("null => halt")]
        public static void IsNotNull(object actual)
        {
            NUnit.Framework.Assert.NotNull(actual);
        }

        [ContractAnnotation("actual:null => halt")]
        public static void IsNotNull(object actual, string propertyName)
        {
            NUnit.Framework.Assert.NotNull(actual, $"'{propertyName}' does not match.");
        }
    }
}
