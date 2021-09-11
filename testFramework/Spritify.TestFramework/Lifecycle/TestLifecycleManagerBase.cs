using System;
using System.Collections.Generic;
using System.Linq;
using Spritify.Common.Extensions;
using Spritify.TestFramework.Interfaces.Lifecycle;
using Spritify.TestFramework.TestContext;

namespace Spritify.TestFramework.Lifecycle
{
    public abstract class TestLifecycleManagerBase<TTestContext, TLifecycleContext, TSetupTestRunParameters, TSetupParameters, TSetupTestParameters, TCleanupTestParameters, TCleanupParameters, TCleanupTestRunParameters> :
        ITestLifecycleManager<TTestContext, TSetupTestRunParameters, TSetupParameters, TSetupTestParameters, TCleanupTestParameters, TCleanupParameters, TCleanupTestRunParameters>
        where TLifecycleContext : ILifecycleContext<TTestContext>, new()
        where TTestContext : TestContextBase, new()
    {
        private readonly List<ITestLifecycleStep<TLifecycleContext, object>> lifecycleSteps;
        private TLifecycleContext rootLifecycleContext;
        private readonly Dictionary<string, TLifecycleContext> lifecycleContextsByTestClassId;
        private readonly Dictionary<string, TLifecycleContext> lifecycleContextsByTestId;

        protected TestLifecycleManagerBase()
        {
            lifecycleContextsByTestClassId = new Dictionary<string, TLifecycleContext>(StringComparer.OrdinalIgnoreCase);
            lifecycleContextsByTestId = new Dictionary<string, TLifecycleContext>(StringComparer.OrdinalIgnoreCase);
            lifecycleSteps = new List<ITestLifecycleStep<TLifecycleContext, object>>();
        }

        public void SetupTestRun(TSetupTestRunParameters parameters)
        {
            RegisterTestLifecycleSteps();
            rootLifecycleContext = new TLifecycleContext();

            ExecuteLifecycleStepsOfType(TestLifecycleStepType.SetupTestRun, rootLifecycleContext, parameters);
        }

        public void Setup(Type testClassType, TSetupParameters parameters)
        {
            var lifecycleContext = (TLifecycleContext)rootLifecycleContext.CloneTestRunProperties();
            var testClassId = GetTestClassId(testClassType);
            lifecycleContextsByTestClassId.Add(testClassId, lifecycleContext);

            ExecuteLifecycleStepsOfType(TestLifecycleStepType.Setup, lifecycleContext, parameters);
        }

        public void SetupTest(Type testClassType, string id, TSetupTestParameters parameters)
        {
            var testClassId = GetTestClassId(testClassType);

            if (!lifecycleContextsByTestClassId.TryGetValue(testClassId, out var testClassLifecycleContext))
            {
                throw new InvalidOperationException($"No context found for test class of type '{testClassType.FullName}'. Make sure that the Setup() method was executed.");
            }

            var lifecycleContext = (TLifecycleContext)testClassLifecycleContext.CloneTestClassProperties();

            lifecycleContext.TestId = id;
            lifecycleContext.Context = new TTestContext();

            lifecycleContextsByTestId.Add(id, lifecycleContext);

            ExecuteLifecycleStepsOfType(TestLifecycleStepType.SetupTest, lifecycleContext, parameters);
        }

        public void CleanupTest(string id, TCleanupTestParameters parameters)
        {
            if (!lifecycleContextsByTestId.TryGetValue(id, out var lifecycleContext))
            {
                throw new InvalidOperationException($"No context found for test with id '{id}'. Make sure the SetupTest() method was executed.");
            }

            ExecuteLifecycleStepsOfType(TestLifecycleStepType.CleanupTest, lifecycleContext, parameters);

            lifecycleContextsByTestId.Remove(id);
        }

        public void Cleanup(Type testClassType, TCleanupParameters parameters)
        {
            var testClassId = GetTestClassId(testClassType);

            if (!lifecycleContextsByTestClassId.TryGetValue(testClassId, out var lifecycleContext))
            {
                throw new InvalidOperationException($"No context found for test class of type '{testClassType.FullName}'. Make sure that the Setup() method was executed.");
            }

            ExecuteLifecycleStepsOfType(TestLifecycleStepType.Cleanup, lifecycleContext, parameters);

            lifecycleContextsByTestClassId.Remove(testClassId);
        }

        public void CleanupTestRun(TCleanupTestRunParameters parameters)
        {
            ExecuteLifecycleStepsOfType(TestLifecycleStepType.CleanupTestRun, rootLifecycleContext, parameters);

            rootLifecycleContext = default;
        }

        public TTestContext GetTestContext(string id)
        {
            return GetLifecycleContext(id).Context;
        }

        protected TLifecycleContext GetLifecycleContext(string id)
        {
            if (!lifecycleContextsByTestId.TryGetValue(id, out var testLifecycleContext))
            {
                throw new ArgumentException($"There is no test with the id '{id}'.");
            }

            return testLifecycleContext;
        }

        protected virtual void RegisterTestLifecycleSteps()
        {
            // override if necessary
        }

        protected void CreateSetupTestRunStep(Action<TLifecycleContext, TSetupTestRunParameters> action)
        {
            var castAction = CastTestLifecycleAction(action);

            lifecycleSteps.Add(TestLifecycleStep<TLifecycleContext, object>.CreateSetupTestRunStep(castAction));
        }

        protected void CreateSetupStep(Action<TLifecycleContext, TSetupParameters> action)
        {
            var castAction = CastTestLifecycleAction(action);

            lifecycleSteps.Add(TestLifecycleStep<TLifecycleContext, object>.CreateSetupStep(castAction));
        }

        protected void CreateSetupTestStep(Action<TLifecycleContext, TSetupTestParameters> action)
        {
            var castAction = CastTestLifecycleAction(action);

            lifecycleSteps.Add(TestLifecycleStep<TLifecycleContext, object>.CreateSetupTestStep(castAction));
        }

        protected void CreateCleanupTestStep(Action<TLifecycleContext, TCleanupTestParameters> action)
        {
            var castAction = CastTestLifecycleAction(action);

            lifecycleSteps.Add(TestLifecycleStep<TLifecycleContext, object>.CreateCleanupTestStep(castAction));
        }

        protected void CreateCleanupStep(Action<TLifecycleContext, TCleanupParameters> action)
        {
            var castAction = CastTestLifecycleAction(action);

            lifecycleSteps.Add(TestLifecycleStep<TLifecycleContext, object>.CreateCleanupStep(castAction));
        }

        protected void CreateCleanupTestRunStep(Action<TLifecycleContext, TCleanupTestRunParameters> action)
        {
            var castAction = CastTestLifecycleAction(action);

            lifecycleSteps.Add(TestLifecycleStep<TLifecycleContext, object>.CreateCleanupTestRunStep(castAction));
        }

        protected Action<TLifecycleContext, object> CastTestLifecycleAction<TParameters>(Action<TLifecycleContext, TParameters> action)
        {
            return (lifecycleContext, parameters) => { action(lifecycleContext, (TParameters)parameters); };
        }

        private string GetTestClassId(Type testClassType)
        {
            return testClassType.GetUniqueKey();
        }

        private void ExecuteLifecycleStepsOfType(TestLifecycleStepType type, TLifecycleContext context, object parameters = null)
        {
            var stepsOfType = lifecycleSteps.Where(step => step.Type == type);

            foreach (var step in stepsOfType)
            {
                step.Execute(context, parameters);
            }
        }
    }
}