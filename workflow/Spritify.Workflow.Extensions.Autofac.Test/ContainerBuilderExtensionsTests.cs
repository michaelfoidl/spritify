using Autofac;
using Spritify.TestFramework;
using Spritify.TestFramework.Assertions;
using Spritify.TestFramework.Assertions.Batch;
using Spritify.TestFramework.Attributes;
using Spritify.Workflow.Extensions.Test.Base;
using Spritify.Workflow.Extensions.Test.Base.TestImplementations;

namespace Spritify.Workflow.Extensions.Autofac.Test
{
    [TestClass]
    public class ContainerBuilderExtensionsTests : WorkflowExtensionTestBase
    {
        [Test]
        [Category(TestCategory.UnitTest)]
        public void RegisterWorkflow_ShouldRegisterWorkflow()
        {
            // Arrange
            var containerBuilder = new ContainerBuilder();
            RegisterWorkflowDependencies(containerBuilder);

            // Act
            containerBuilder.RegisterWorkflow<TestWorkflow>();

            // Assert
            var container = containerBuilder.Build();

            var workflowManager = container.Resolve<IWorkflowManager>();
            Assert.IsNotNull(workflowManager);
            
            workflowManager.HasInfo(TestWorkflow.StepIdentifier); // check if step exists
        }

        [Test]
        [Category(TestCategory.UnitTest)]
        public void RegisterWorkflow_ShouldRegisterWorkflowAsSingleton()
        {
            // Arrange
            var containerBuilder = new ContainerBuilder();
            RegisterWorkflowDependencies(containerBuilder);

            // Act
            containerBuilder.RegisterWorkflow<TestWorkflow>();

            // Assert
            var container = containerBuilder.Build();

            var workflowManagers = ExecuteConsecutively(() => container.Resolve<IWorkflowManager>(), 2);
            BatchAssert.AssertSimple(Assert.AreEqual, workflowManagers);
        }

        private void RegisterWorkflowDependencies(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterInstance(ContextComposerProviderMock.Object).As<IWorkflowContextComposerProvider>().SingleInstance();
        }
    }
}
