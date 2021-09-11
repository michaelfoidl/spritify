using System;
using System.Text;

namespace Spritify.TestFramework.Assertions.Exception
{
    public class ExceptionAssert
    {
        public static void Throws(Action action)
        {
            Throws<System.Exception>(action);
        }

        public static void Throws<TException>(Action action) where TException : System.Exception
        {
            try
            {
                action();
                NUnit.Framework.Assert.Fail($"Expected exception of type '{typeof(TException).FullName}', but none was thrown.");
            }
            catch (TException e)
            {
                if (e is NUnit.Framework.AssertionException)
                {
                    throw;
                }
            }
            catch (System.Exception e)
            {
                if (e is NUnit.Framework.AssertionException)
                {
                    throw;
                }

                var failureMessageBuilder = new StringBuilder();
                failureMessageBuilder.Append("Expected exception of type '");
                failureMessageBuilder.Append(typeof(TException).FullName);
                failureMessageBuilder.Append("', but exception of type '");
                failureMessageBuilder.Append(e.GetType().FullName);
                failureMessageBuilder.Append("' was thrown:");
                failureMessageBuilder.Append(Environment.NewLine);
                failureMessageBuilder.Append("Message: ");
                failureMessageBuilder.Append(e.Message);
                failureMessageBuilder.Append(Environment.NewLine);
                failureMessageBuilder.Append("InnerException: ");
                failureMessageBuilder.Append(e.InnerException?.Message);
                failureMessageBuilder.Append(Environment.NewLine);
                failureMessageBuilder.Append("StackTrace:");
                failureMessageBuilder.Append(Environment.NewLine);
                failureMessageBuilder.Append(e.StackTrace);

                NUnit.Framework.Assert.Fail(failureMessageBuilder.ToString());
            }
        }
    }
}