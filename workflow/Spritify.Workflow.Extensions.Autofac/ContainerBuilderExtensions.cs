using Autofac;

namespace Spritify.Workflow.Extensions.Autofac
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterWorkflow<TWorkflow>(this ContainerBuilder builder)
            where TWorkflow : WorkflowBase, new()
        {
            var workflow = new TWorkflow();
            builder.Register(context => workflow.Create(context.Resolve<IWorkflowContextComposerProvider>()));
        }
    }
}