using Spritify.TestFramework.Attributes;
using Spritify.TestFramework.CurrentContext;
using Spritify.TestFramework.Interfaces.Lifecycle;
using Spritify.TestFramework.Lifecycle;
using Spritify.TestFramework.TestContext;

namespace Spritify.TestFramework
{
    public abstract class ManagedTest<TLifecycleManager, TTestContext, TSetupTestRunParameters, TSetupParameters, TSetupTestParameters, TCleanupTestParameters, TCleanupParameters, TCleanupTestRunParameters> :
        UnmanagedTest
        where TLifecycleManager : class, ITestLifecycleManager<TTestContext, TSetupTestRunParameters, TSetupParameters, TSetupTestParameters, TCleanupTestParameters, TCleanupParameters, TCleanupTestRunParameters>, new()
        where TTestContext : TestContextBase, new()
    {
        protected static string Id => CurrentTest.Id;
        protected TTestContext Context => LifecycleManager.GetTestContext(Id);

        protected TLifecycleManager LifecycleManager =>
            TestRun<TLifecycleManager, TTestContext, TSetupTestRunParameters, TSetupParameters, TSetupTestParameters, TCleanupTestParameters, TCleanupParameters, TCleanupTestRunParameters>.Instance.CurrentLifecycleManager;

        [OneTimeSetup]
        public sealed override void SetupTestClass()
        {
            LifecycleManager.Setup(GetType(), GetSetupParameters());
        }

        [Setup]
        public sealed override void SetupTest()
        {
            LifecycleManager.SetupTest(GetType(), Id, GetSetupTestParameters());
        }

        [Teardown]
        public sealed override void CleanupTest()
        {
            LifecycleManager.CleanupTest(Id, GetCleanupTestParameters());
        }

        [OneTimeTeardown]
        public sealed override void CleanupTestClass()
        {
            LifecycleManager.Cleanup(GetType(), GetCleanupParameters());
        }

        protected virtual TSetupParameters GetSetupParameters()
        {
            return default;
        }

        protected virtual TSetupTestParameters GetSetupTestParameters()
        {
            return default;
        }

        protected virtual TCleanupTestParameters GetCleanupTestParameters()
        {
            return default;
        }

        protected virtual TCleanupParameters GetCleanupParameters()
        {
            return default;
        }
    }
}