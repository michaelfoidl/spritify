namespace Spritify.Workflow.Extensions.Test.Base.TestImplementations
{
    public class TestWorkflow : WorkflowBase
    {
        public const string StepIdentifier = "MyStep";

        protected override void Configure(IWorkflowDefinitionBuilder builder)
        {
            builder.AddStep(step => step
                .WithIdentifier(StepIdentifier));
        }
    }
}
