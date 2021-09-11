using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Spritify.Common;
using IConfigurationBuilder = Spritify.TestFramework.Extensions.Configuration.Interfaces.IConfigurationBuilder;

namespace Spritify.TestFramework.Extensions.Configuration
{
    public class ConfigurationBuilder : IConfigurationBuilder
    {
        private readonly Dictionary<string, string> settings;

        public ConfigurationBuilder()
        {
            settings = new Dictionary<string, string>();
        }

        public Microsoft.Extensions.Configuration.IConfiguration Build()
        {
            var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
            var configuration = builder
                .AddInMemoryCollection(settings)
                .Build();

            return configuration;
        }

        private IConfigurationBuilder AddSetting(string key, string value)
        {
            Ensure.ArgumentIsNotNullEmptyOrWhitespace(key, nameof(key));

            settings.Add(key, value);

            return this;
        }
    }
}
