using System;

namespace Spritify.TestFramework.Assertions.String
{
    public static class StringAssert
    {
        public static void Contains(string expectedPart, string actual)
        {
            if (!ContainsInternal(expectedPart, actual))
            {
                NUnit.Framework.Assert.Fail("String does not contain the expected substring.");
            }
        }

        public static void DoesNotContain(string forbiddenPart, string actual)
        {
            if (ContainsInternal(forbiddenPart, actual))
            {
                NUnit.Framework.Assert.Fail("String does contain the forbidden substring.");
            }
        }

        public static void StartsWith(string expectedPart, string actual)
        {
            if (!StartsWithInternal(expectedPart, actual))
            {
                NUnit.Framework.Assert.Fail("String does not start with the expected substring.");
            }
        }

        public static void DoesNotStartWith(string expectedPart, string actual)
        {
            if (StartsWithInternal(expectedPart, actual))
            {
                NUnit.Framework.Assert.Fail("String does start with the forbidden substring.");
            }
        }

        public static void IsEmpty(string actual)
        {
            if (!string.IsNullOrEmpty(actual))
            {
                NUnit.Framework.Assert.Fail("String is not null or empty.");
            }
        }

        public static void IsNotEmpty(string actual)
        {
            if (string.IsNullOrEmpty(actual))
            {
                NUnit.Framework.Assert.Fail("String is null or empty.");
            }
        }

        private static bool ContainsInternal(string expectedPart, string actual)
        {
            return actual.Contains(expectedPart, StringComparison.OrdinalIgnoreCase);
        }

        private static bool StartsWithInternal(string expectedPart, string actual)
        {
            return actual.StartsWith(expectedPart, StringComparison.OrdinalIgnoreCase);
        }
    }
}
