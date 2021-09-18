using Spritify.Workflow.Internal;

namespace Spritify.Workflow
{
    public abstract class WorkflowBase
    {
        public void Setup(IWorkflowContextComposerProvider contextComposerProvider)
        {
            var workflowBuilder = new WorkflowDefinitionBuilder();
            Configure(workflowBuilder);
            var workflow = workflowBuilder.Build();

            var workflowManager = new WorkflowManager(workflow, contextComposerProvider);
            Register(workflowManager);
        }

        /// <summary>
        /// Registers the provided instance of IWorkflowManager as singleton.
        /// </summary>
        protected abstract void Register(IWorkflowManager workflowManager);

        /// <summary>
        /// Configures the workflow.
        /// </summary>
        protected abstract void Configure(IWorkflowDefinitionBuilder builder);
    }
}