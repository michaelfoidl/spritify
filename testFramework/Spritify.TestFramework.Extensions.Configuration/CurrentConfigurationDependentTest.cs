using Spritify.TestFramework.Extensions.Configuration.Attributes;

namespace Spritify.TestFramework.Extensions.Configuration
{
    public class CurrentConfigurationDependentTest
    {
        public static string GetCurrentConfigurationKey()
        {
            return NUnit.Framework.TestContext.CurrentContext.Test.Properties.Get(UseConfigurationAttribute.UseConfigurationKey) as string;
        }
    }
}
