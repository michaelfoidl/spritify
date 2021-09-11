using Microsoft.Extensions.Configuration;
using Spritify.TestFramework.Interfaces.Lifecycle;

namespace Spritify.TestFramework.Extensions.Configuration.Interfaces
{
    public interface IConfigurationDependentLifecycleContext<TTestContext> : ILifecycleContext<TTestContext>
    {
        public IConfiguration Configuration { get; set;}
    }
}
