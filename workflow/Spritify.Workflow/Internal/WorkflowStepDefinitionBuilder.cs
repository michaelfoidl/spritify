using System.Collections.Generic;
using Spritify.Common;

namespace Spritify.Workflow.Internal
{
    internal class WorkflowStepDefinitionBuilder : IWorkflowStepDefinitionBuilder
    {
        private readonly WorkflowStepDefinition definition;

        public WorkflowStepDefinitionBuilder()
        {
            definition = new WorkflowStepDefinition
            {
                DependentOn = new List<string>()
            };
        }

        public IWorkflowStepDefinitionBuilder WithIdentifier(string identifier)
        {
            definition.Identifier = identifier;

            return this;
        }

        public IWorkflowStepDefinitionBuilder DependentOn(string identifier)
        {
            definition.DependentOn.Add(identifier);

            return this;
        }

        public WorkflowStepDefinition Build()
        {
            Validate();

            return definition;
        }

        private void Validate()
        {
            Ensure.ArgumentIsNotNullEmptyOrWhitespace(definition.Identifier, "Identifier");
        }
    }
}