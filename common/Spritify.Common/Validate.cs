using System.Text.RegularExpressions;

namespace Spritify.Common
{
    public class Validate
    {
        private const string EmailRegex = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@(([\-\w]+\.)+[a-zA-Z]{2,})\z";

        public static bool ArgumentIsPositiveInteger(int? argument, string argumentName, out string validationError)
        {
            if (!ArgumentIsNotNull(argument, argumentName, out var isNullValidationError))
            {
                validationError = isNullValidationError;
                return false;
            }

            if (argument <= 0)
            {
                validationError = CreateErrorMessage(argumentName, "must be a positive integer");
                return false;
            }

            validationError = null;
            return true;
        }

        public static bool ArgumentIsNotNull(object argument, string argumentName, out string validationError)
        {
            if (argument == null)
            {
                validationError = CreateErrorMessage(argumentName, "must not be null");
                return false;
            }

            validationError = null;
            return true;
        }

        public static bool ArgumentIsNotEmptyOrWhitespace(string argument, string argumentName, out string validationError)
        {
            if (argument != null && string.IsNullOrWhiteSpace(argument))
            {
                validationError = CreateErrorMessage(argumentName, "must not be empty");
                return false;
            }

            validationError = null;
            return true;
        }

        public static bool ArgumentIsNotNullEmptyOrWhitespace(string argument, string argumentName, out string validationError)
        {
            if (!ArgumentIsNotNull(argument, argumentName, out var isNullValidationError))
            {
                validationError = isNullValidationError;
                return false;
            }

            if (!ArgumentIsNotEmptyOrWhitespace(argument, argumentName, out var isEmptyOrWhitespaceValidationError))
            {
                validationError = isEmptyOrWhitespaceValidationError;
                return false;
            }

            validationError = null;
            return true;
        }

        public static bool ArgumentIsEmailAddress(string argument, string argumentName, out string validationError)
        {
            if (!ArgumentIsNotNullEmptyOrWhitespace(argument, argumentName, out var isEmptyOrWhitespaceValidationError))
            {
                validationError = isEmptyOrWhitespaceValidationError;
                return false;
            }

            if (!Regex.IsMatch(argument, EmailRegex))
            {
                validationError = CreateErrorMessage(argumentName, "is not a valid e-mail address.");
                return false;
            }

            validationError = null;
            return true;
        }

        private static string CreateErrorMessage(string argumentName, string expectedCondition)
        {
            return argumentName != null
                ? $"{argumentName} {expectedCondition}."
                : $"Argument {expectedCondition}.";
        }
    }
}