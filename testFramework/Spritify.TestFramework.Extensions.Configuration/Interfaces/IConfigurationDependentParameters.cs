using System;

namespace Spritify.TestFramework.Extensions.Configuration.Interfaces
{
    public interface IConfigurationDependentParameters
    {
        public Action<string, IConfigurationBuilder> ConfigurationSetupAction { get; set; }
    }
}