using Spritify.TestFramework.Interfaces.Lifecycle;
using Spritify.TestFramework.TestContext;

namespace Spritify.TestFramework.Lifecycle
{
    public class TestRun<TLifecycleManager, TTestContext, TSetupTestRunParameters, TSetupParameters, TSetupTestParameters, TCleanupTestParameters, TCleanupParameters, TCleanupTestRunParameters>
        where TLifecycleManager : class, ITestLifecycleManager<TTestContext, TSetupTestRunParameters, TSetupParameters, TSetupTestParameters, TCleanupTestParameters, TCleanupParameters, TCleanupTestRunParameters>
        where TTestContext : TestContextBase, new()
    {
        private static TestRun<TLifecycleManager, TTestContext, TSetupTestRunParameters, TSetupParameters, TSetupTestParameters, TCleanupTestParameters, TCleanupParameters, TCleanupTestRunParameters> instance;

        public static TestRun<TLifecycleManager, TTestContext, TSetupTestRunParameters, TSetupParameters, TSetupTestParameters, TCleanupTestParameters, TCleanupParameters, TCleanupTestRunParameters> Instance =>
            instance ??= new TestRun<TLifecycleManager, TTestContext, TSetupTestRunParameters, TSetupParameters, TSetupTestParameters, TCleanupTestParameters, TCleanupParameters, TCleanupTestRunParameters>();

        public TLifecycleManager CurrentLifecycleManager { get; private set; }

        public void Start(TLifecycleManager lifecycleManager)
        {
            CurrentLifecycleManager = lifecycleManager;
        }

        public void End()
        {
            CurrentLifecycleManager = null;
        }
    }
}