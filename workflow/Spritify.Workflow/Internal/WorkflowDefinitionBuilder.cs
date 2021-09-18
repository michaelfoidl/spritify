using System;
using System.Collections.Generic;

namespace Spritify.Workflow.Internal
{
    internal class WorkflowDefinitionBuilder : IWorkflowDefinitionBuilder
    {
        private readonly WorkflowDefinition definition;

        public WorkflowDefinitionBuilder()
        {
            definition = new WorkflowDefinition
            {
                Steps = new List<WorkflowStepDefinition>()
            };
        }

        public IWorkflowDefinitionBuilder AddStep(Action<IWorkflowStepDefinitionBuilder> builder)
        {
            var stepBuilder = new WorkflowStepDefinitionBuilder();
            builder(stepBuilder);
            var step = stepBuilder.Build();

            definition.Steps.Add(step);

            return this;
        }

        public WorkflowDefinition Build()
        {
            return definition;
        }
    }
}