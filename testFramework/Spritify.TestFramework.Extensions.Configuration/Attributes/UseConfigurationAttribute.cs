using System;

namespace Spritify.TestFramework.Extensions.Configuration.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class UseConfigurationAttribute : Attribute, NUnit.Framework.Interfaces.IApplyToTest
    {
        public static string UseConfigurationKey = "UseConfiguration";

        private readonly string configurationKey;

        public UseConfigurationAttribute(string configurationKey)
        {
            this.configurationKey = configurationKey;
        }

        public void ApplyToTest(NUnit.Framework.Internal.Test test)
        {
            test.Properties.Add(UseConfigurationKey, configurationKey);
        }
    }
}
