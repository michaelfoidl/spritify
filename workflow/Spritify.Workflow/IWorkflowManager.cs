namespace Spritify.Workflow
{
    public interface IWorkflowManager
    {
        bool HasInfo(string identifier);
        void StoreInfo(string identifier, IWorkflowInfo info);

        TContext AccessContext<TContext>(string identifier)
            where TContext : class, IWorkflowContext;
    }
}