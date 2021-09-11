using Spritify.TestFramework.Extensions.Configuration.Interfaces;

namespace Spritify.TestFramework.Extensions.Configuration
{
    public class ConfigurationDependentTestHelper<TTestContext, TLifecycleContext, TParameters>
    where TLifecycleContext : IConfigurationDependentLifecycleContext<TTestContext>
    where TParameters: IConfigurationDependentParameters
    {
        public void SetupConfiguration(TLifecycleContext lifecycleContext, TParameters parameters)
        {
            var builder = new ConfigurationBuilder();
            var configurationKey = CurrentConfigurationDependentTest.GetCurrentConfigurationKey();
            parameters.ConfigurationSetupAction(configurationKey, builder);
            lifecycleContext.Configuration = builder.Build();
        }
    }
}
