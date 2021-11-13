using Microsoft.Extensions.DependencyInjection;

namespace Spritify.Workflow.Extensions.Microsoft.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddWorkflow<TWorkflow>(this IServiceCollection serviceCollection)
            where TWorkflow : WorkflowBase, new()
        {
            var workflow = new TWorkflow();
            serviceCollection.AddSingleton(
                serviceProvider => workflow.Create(serviceProvider.GetRequiredService<IWorkflowContextComposerProvider>()));
        }
    }
}