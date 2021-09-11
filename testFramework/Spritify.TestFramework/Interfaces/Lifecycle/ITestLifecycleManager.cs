using System;
using Spritify.TestFramework.TestContext;

namespace Spritify.TestFramework.Interfaces.Lifecycle
{
    public interface ITestLifecycleManager<out TTestContext, in TSetupTestRunParameters, in TSetupParameters, in TSetupTestParameters, in TCleanupTestParameters, in TCleanupParameters, in TCleanupTestRunParameters>
        where TTestContext : TestContextBase, new()
    {
        void SetupTestRun(TSetupTestRunParameters parameters);
        void Setup(Type testClassType, TSetupParameters parameters);
        void SetupTest(Type testClassType, string id, TSetupTestParameters parameters);
        void CleanupTest(string id, TCleanupTestParameters parameters);
        void Cleanup(Type testClassType, TCleanupParameters parameters);
        void CleanupTestRun(TCleanupTestRunParameters parameters);
        TTestContext GetTestContext(string id);
    }
}