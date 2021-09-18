using System;

namespace Spritify.Workflow
{
    public class WorkflowException : Exception
    {
        public WorkflowException(string message) : base(message) { }
    }
}
