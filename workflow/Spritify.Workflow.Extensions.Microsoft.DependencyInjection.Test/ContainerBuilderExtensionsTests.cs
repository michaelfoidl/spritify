using Microsoft.Extensions.DependencyInjection;
using Spritify.TestFramework;
using Spritify.TestFramework.Assertions;
using Spritify.TestFramework.Assertions.Batch;
using Spritify.TestFramework.Attributes;
using Spritify.Workflow.Extensions.Test.Base;
using Spritify.Workflow.Extensions.Test.Base.TestImplementations;

namespace Spritify.Workflow.Extensions.Microsoft.DependencyInjection.Test
{
    [TestClass]
    public class ContainerBuilderExtensionsTests : WorkflowExtensionTestBase
    {
        [Test]
        [Category(TestCategory.UnitTest)]
        public void RegisterWorkflow_ShouldRegisterWorkflow()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            RegisterWorkflowDependencies(serviceCollection);

            // Act
            serviceCollection.AddWorkflow<TestWorkflow>();

            // Assert
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var workflowManager = serviceProvider.GetService<IWorkflowManager>();
            Assert.IsNotNull(workflowManager);

            workflowManager.HasInfo(TestWorkflow.StepIdentifier); // check if step exists
        }

        [Test]
        [Category(TestCategory.UnitTest)]
        public void RegisterWorkflow_ShouldRegisterWorkflowAsSingleton()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            RegisterWorkflowDependencies(serviceCollection);

            // Act
            serviceCollection.AddWorkflow<TestWorkflow>();

            // Assert
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var workflowManagers = ExecuteConsecutively(() => serviceProvider.GetService<IWorkflowManager>(), 2);
            BatchAssert.AssertSimple(Assert.AreEqual, workflowManagers);
        }

        private void RegisterWorkflowDependencies(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(_ => ContextComposerProviderMock.Object);
        }
    }
}
