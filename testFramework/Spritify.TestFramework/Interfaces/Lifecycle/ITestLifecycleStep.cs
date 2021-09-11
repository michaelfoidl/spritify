using Spritify.TestFramework.Lifecycle;

namespace Spritify.TestFramework.Interfaces.Lifecycle
{
    public interface ITestLifecycleStep<in TLifecycleContext, in TParameters>
        where TParameters : class
    {
        TestLifecycleStepType Type { get; }
        void Execute(TLifecycleContext context, TParameters parameters = null);
    }
}