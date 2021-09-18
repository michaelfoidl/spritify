using System;

namespace Spritify.Workflow
{
    public interface IWorkflowDefinitionBuilder
    {
        IWorkflowDefinitionBuilder AddStep(Action<IWorkflowStepDefinitionBuilder> builder);
    }
}