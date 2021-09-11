using Spritify.TestFramework.Interfaces.Lifecycle;

namespace Spritify.TestFramework.Extensions.Mocking.Interfaces
{
    public interface IMockingDependentLifecycleContext<TTestContext> : ILifecycleContext<TTestContext>
    {
        public IMockStore MockStore { get; set;}
    }
}
