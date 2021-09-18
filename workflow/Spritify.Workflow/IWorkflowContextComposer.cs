using System.Collections.Generic;

namespace Spritify.Workflow
{
    public interface IWorkflowContextComposer { }

    public interface IWorkflowContextComposer<out TContext> : IWorkflowContextComposer
        where TContext : IWorkflowContext
    {
        TContext Compose(IEnumerable<IWorkflowInfo> infos);
    }
}