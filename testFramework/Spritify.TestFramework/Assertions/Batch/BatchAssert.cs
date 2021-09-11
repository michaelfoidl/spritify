using System;

namespace Spritify.TestFramework.Assertions.Batch
{
    public class BatchAssert
    {
        public static void AssertSimple<T>(Action<T> assertion, params T[] objects)
        {
            foreach (var o in objects)
            {
                assertion(o);
            }
        }

        public static void AssertSimple<T>(Action<T, T> assertion, params T[] objects)
        {
            ExecuteBatchAssertion(objects, assertion, false);
        }

        public static void AssertExhaustive<T>(Action<T, T> assertion, params T[] objects)
        {
            ExecuteBatchAssertion(objects, assertion, true);
        }

        /// <summary>
        /// Executes the given assertion for the given elements.
        /// In exhaustive mode, every possible combination of objects is asserted. This is necessary for e.g. asserting that a list does not contain the same element twice.
        /// In non-exhaustive mode, the first object is compared with every other object. This is sufficient for e.g. asserting that all elements in a list are equal.
        /// </summary>
        private static void ExecuteBatchAssertion<T>(T[] objects, Action<T, T> assertion, bool exhaustive)
        {
            if (objects.Length <= 1)
            {
                return;
            }
            
            var rounds = exhaustive
                ? objects.Length - 1
                : 1;

            // assert every possible combination of objects
            for (int round = 0; round < rounds; round++)
            {
                var referenceObject = objects[round];

                for (int i = round + 1; i < objects.Length; i++)
                {
                    var currentObject = objects[i];
                    assertion(referenceObject, currentObject);
                }
            }
        }
    }
}
