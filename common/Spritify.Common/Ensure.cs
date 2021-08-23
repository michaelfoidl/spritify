using System;

namespace Spritify.Common
{
    public class Ensure
    {
        public static void IsNotDisposed(bool disposed)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(null);
            }
        }

        public static void IsNotNull(object nullable, string nullableName)
        {
            if (nullable == null)
            {
                throw new InvalidOperationException($"Value '{nullableName}' is null, but was not expected to be.");
            }
        }

        public static void ArgumentIsNotNull(object argument, string argumentName)
        {
            if (!Validate.ArgumentIsNotNull(argument, argumentName, out _))
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void ArgumentIsNotEmptyOrWhitespace(string argument, string argumentName)
        {
            if (argument != null && !Validate.ArgumentIsNotNullEmptyOrWhitespace(argument, argumentName, out var validationError))
            {
                throw new ArgumentException(validationError, argumentName);
            }
        }

        public static void ArgumentIsNotNullEmptyOrWhitespace(string argument, string argumentName)
        {
            ArgumentIsNotNull(argument, argumentName);
            ArgumentIsNotEmptyOrWhitespace(argument, argumentName);
        }
    }
}
