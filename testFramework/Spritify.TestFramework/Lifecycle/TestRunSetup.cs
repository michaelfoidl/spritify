using Spritify.TestFramework.Attributes;
using Spritify.TestFramework.Interfaces.Lifecycle;
using Spritify.TestFramework.TestContext;

namespace Spritify.TestFramework.Lifecycle
{
    public abstract class TestRunSetupBase<TLifecycleManager, TTestContext, TSetupTestRunParameters, TSetupParameters, TSetupTestParameters, TCleanupTestParameters, TCleanupParameters, TCleanupTestRunParameters>
        where TLifecycleManager : class, ITestLifecycleManager<TTestContext, TSetupTestRunParameters, TSetupParameters, TSetupTestParameters, TCleanupTestParameters, TCleanupParameters, TCleanupTestRunParameters>, new()
        where TTestContext : TestContextBase, new()
    {
        private static TestRun<TLifecycleManager, TTestContext, TSetupTestRunParameters, TSetupParameters, TSetupTestParameters, TCleanupTestParameters, TCleanupParameters, TCleanupTestRunParameters> TestRun =>
            TestRun<TLifecycleManager, TTestContext, TSetupTestRunParameters, TSetupParameters, TSetupTestParameters, TCleanupTestParameters, TCleanupParameters, TCleanupTestRunParameters>.Instance;

        [OneTimeSetup]
        public void SetupTestRun()
        {
            var lifecycleManager = new TLifecycleManager();
            TestRun.Start(lifecycleManager);
            TestRun.CurrentLifecycleManager.SetupTestRun(GetSetupTestRunParameters());

        }

        [OneTimeTeardown]
        public void CleanupTestRun()
        {
            TestRun.CurrentLifecycleManager.CleanupTestRun(GetCleanupTestRunParameters());
            TestRun.End();
        }

        protected virtual TSetupTestRunParameters GetSetupTestRunParameters()
        {
            return default;
        }

        protected virtual TCleanupTestRunParameters GetCleanupTestRunParameters()
        {
            return default;
        }
    }
}