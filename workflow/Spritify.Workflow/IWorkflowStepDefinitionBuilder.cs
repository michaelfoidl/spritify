using Spritify.Workflow.Internal;

namespace Spritify.Workflow
{
    public interface IWorkflowStepDefinitionBuilder
    {
        IWorkflowStepDefinitionBuilder WithIdentifier(string identifier);
        IWorkflowStepDefinitionBuilder DependentOn(string identifier);
    }
}