using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Spritify.Workflow.Internal
{
    internal class WorkflowManager : IWorkflowManager
    {
        private readonly WorkflowDefinition definition;
        private readonly IWorkflowContextComposerProvider contextComposerProvider;
        private readonly Dictionary<string, AsyncLocal<IWorkflowInfo>> infoStore;
        private readonly Dictionary<string, AsyncLocal<IWorkflowContext>> contextStore;

        public WorkflowManager(
            WorkflowDefinition definition,
            IWorkflowContextComposerProvider contextComposerProvider)
        {
            this.definition = definition;
            this.contextComposerProvider = contextComposerProvider;

            infoStore = new Dictionary<string, AsyncLocal<IWorkflowInfo>>();
            contextStore = new Dictionary<string, AsyncLocal<IWorkflowContext>>();

            InitializeStores();
        }

        public bool HasInfo(string identifier)
        {
            EnsureStep(identifier);

            var hasInfo = infoStore[identifier].Value != null;
            return hasInfo;
        }

        public void StoreInfo(string identifier, IWorkflowInfo info)
        {
            EnsureStep(identifier);

            if (HasInfo(identifier))
            {
                throw new WorkflowException($"Multiple infos for a single workflow step: \"{identifier}\".");
            }

            infoStore[identifier].Value = info;
        }

        public TContext AccessContext<TContext>(string identifier)
            where TContext : class, IWorkflowContext
        {
            EnsureStep(identifier);

            var existingContext = contextStore[identifier].Value;
            if (existingContext != null)
            {
                return existingContext as TContext;
            }

            var context = CreateContext<TContext>(identifier);

            contextStore[identifier].Value = context;

            return context;
        }

        private void InitializeStores()
        {
            foreach (var step in definition.Steps)
            {
                infoStore.Add(step.Identifier, new AsyncLocal<IWorkflowInfo>());
                contextStore.Add(step.Identifier, new AsyncLocal<IWorkflowContext>());
            }
        }

        private TContext CreateContext<TContext>(string identifier) where TContext : class, IWorkflowContext
        {
            var infos = GetNecessaryWorkflowInfos(identifier).ToList();

            var composer = contextComposerProvider.Provide<TContext>(identifier);
            if (composer == null)
            {
                throw new WorkflowException($"Could not find composer for workflow step: \"{identifier}\".");
            }

            var context = composer.Compose(infos);
            
            return context;
        }

        private IEnumerable<IWorkflowInfo> GetNecessaryWorkflowInfos(string identifier)
        {
            var step = EnsureStep(identifier);

            yield return EnsureInfo(identifier);

            foreach (var dependentInfo in step.DependentOn.SelectMany(GetNecessaryWorkflowInfos))
            {
                yield return dependentInfo;
            }
        }

        private WorkflowStepDefinition EnsureStep(string identifier)
        {
            var step = definition.Steps.FirstOrDefault(s => s.Identifier == identifier);

            if (step == null)
            {
                throw new WorkflowException($"The workflow definition does not contain a step \"{identifier}\".");
            }

            return step;
        }

        private IWorkflowInfo EnsureInfo(string stepIdentifier)
        {
            if (HasInfo(stepIdentifier))
            {
                return infoStore[stepIdentifier].Value;
            }

            throw new WorkflowException($"No info has been added for step \"{stepIdentifier}\".");
        }
    }
}