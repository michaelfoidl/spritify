using Spritify.Workflow.Internal;

namespace Spritify.Workflow
{
    public abstract class WorkflowBase
    {
        public IWorkflowManager Create(IWorkflowContextComposerProvider contextComposerProvider)
        {
            var workflowBuilder = new WorkflowDefinitionBuilder();
            Configure(workflowBuilder);
            var workflow = workflowBuilder.Build();

            var workflowManager = new WorkflowManager(workflow, contextComposerProvider);
            return workflowManager;
        }

        /// <summary>
        /// Configures the workflow.
        /// </summary>
        protected abstract void Configure(IWorkflowDefinitionBuilder builder);
    }
}