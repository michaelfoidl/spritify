namespace Spritify.Workflow
{
    public interface IWorkflowContextComposerProvider
    {
        IWorkflowContextComposer<TContext> Provide<TContext>(string identifier)
            where TContext : IWorkflowContext;
    }
}