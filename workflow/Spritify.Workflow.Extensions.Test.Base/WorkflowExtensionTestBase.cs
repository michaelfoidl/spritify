using Moq;
using Spritify.TestFramework;
using Spritify.TestFramework.Extensions.Mocking;
using Spritify.TestFramework.Extensions.Mocking.Interfaces;

namespace Spritify.Workflow.Extensions.Test.Base
{
    public abstract class WorkflowExtensionTestBase : UnmanagedTest, IMockingDependentTest
    {
        public IMockStore MockStore { get; private set; }

        protected Mock<IWorkflowContextComposerProvider> ContextComposerProviderMock;

        public override void SetupTest()
        {
            base.SetupTest();

            MockStore = new MockStore();

            ContextComposerProviderMock = this.GetOrCreateMock<IWorkflowContextComposerProvider>();
        }
    }
}