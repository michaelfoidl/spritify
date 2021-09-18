using System.Collections.Generic;

namespace Spritify.Workflow.Internal
{
    internal class WorkflowStepDefinition
    {
        public string Identifier { get; set; }
        public List<string> DependentOn { get; set; }
    }
}