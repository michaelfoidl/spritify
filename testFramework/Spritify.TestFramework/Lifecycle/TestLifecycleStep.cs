using System;
using System.Diagnostics;
using Spritify.TestFramework.Interfaces.Lifecycle;

namespace Spritify.TestFramework.Lifecycle
{
    public class TestLifecycleStep<TLifecycleContext, TParameters> : ITestLifecycleStep<TLifecycleContext, TParameters>
        where TParameters : class
    {
        private readonly Action<TLifecycleContext, TParameters> stepAction;
        
        public TestLifecycleStepType Type { get; }

        private TestLifecycleStep(TestLifecycleStepType type, Action<TLifecycleContext, TParameters> stepAction)
        {
            Type = type;
            this.stepAction = stepAction;
        }

        [DebuggerStepThrough]
        public void Execute(TLifecycleContext context, TParameters parameters)
        {
            stepAction(context, parameters);
        }

        public static TestLifecycleStep<TLifecycleContext, TParameters> CreateSetupTestRunStep(Action<TLifecycleContext, TParameters> stepAction)
        {
            return new TestLifecycleStep<TLifecycleContext, TParameters>(TestLifecycleStepType.SetupTestRun, stepAction);
        }

        public static TestLifecycleStep<TLifecycleContext, TParameters> CreateSetupStep(Action<TLifecycleContext, TParameters> stepAction)
        {
            return new TestLifecycleStep<TLifecycleContext, TParameters>(TestLifecycleStepType.Setup, stepAction);
        }

        public static TestLifecycleStep<TLifecycleContext, TParameters> CreateSetupTestStep(Action<TLifecycleContext, TParameters> stepAction)
        {
            return new TestLifecycleStep<TLifecycleContext, TParameters>(TestLifecycleStepType.SetupTest, stepAction);
        }

        public static TestLifecycleStep<TLifecycleContext, TParameters> CreateCleanupTestStep(Action<TLifecycleContext, TParameters> stepAction)
        {
            return new TestLifecycleStep<TLifecycleContext, TParameters>(TestLifecycleStepType.CleanupTest, stepAction);
        }

        public static TestLifecycleStep<TLifecycleContext, TParameters> CreateCleanupStep(Action<TLifecycleContext, TParameters> stepAction)
        {
            return new TestLifecycleStep<TLifecycleContext, TParameters>(TestLifecycleStepType.Cleanup, stepAction);
        }

        public static TestLifecycleStep<TLifecycleContext, TParameters> CreateCleanupTestRunStep(Action<TLifecycleContext, TParameters> stepAction)
        {
            return new TestLifecycleStep<TLifecycleContext, TParameters>(TestLifecycleStepType.CleanupTestRun, stepAction);
        }
    }
}