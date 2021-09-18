using System;
using System.Collections.Generic;
using Moq;
using Spritify.TestFramework;
using Spritify.TestFramework.Assertions;
using Spritify.TestFramework.Assertions.Batch;
using Spritify.TestFramework.Attributes;
using Spritify.TestFramework.Extensions.Mocking;
using Spritify.TestFramework.Extensions.Mocking.Interfaces;
using Spritify.Workflow.Internal;

namespace Spritify.Workflow.Test
{
    [TestClass]
    public class WorkflowManagerTests : UnmanagedTest, IMockingDependentTest
    {
        private IWorkflowManager workflowManager;

        public IMockStore MockStore { get; private set; }

        private const string FirstStepIdentifier = "FirstStep";
        private const string SecondStepIdentifier = "SecondStep";
        private const string ThirdStepIdentifier = "ThirdStep";

        public override void SetupTest()
        {
            base.SetupTest();

            MockStore = new MockStore();

            var workflowDefinition = new WorkflowDefinition
            {
                Steps = new List<WorkflowStepDefinition>
                {
                    new()
                    {
                        Identifier = FirstStepIdentifier,
                        DependentOn = new List<string>()
                    },
                    new()
                    {
                        Identifier = SecondStepIdentifier,
                        DependentOn = new List<string> { FirstStepIdentifier }
                    },
                    new()
                    {
                        Identifier = ThirdStepIdentifier,
                        DependentOn = new List<string> { SecondStepIdentifier }
                    }
                }
            };

            var contextComposerProviderMock = this.GetOrCreateMock<IWorkflowContextComposerProvider>();

            workflowManager = new WorkflowManager(workflowDefinition, contextComposerProviderMock.Object);
        }

        [Test]
        [Category(TestCategory.UnitTest)]
        public void StoreInfo_IndependentStep_ShouldStoreStepInfo()
        {
            // Arrange
            var infoMock = this.GetOrCreateMock<IWorkflowInfo>();

            // Act
            workflowManager.StoreInfo(FirstStepIdentifier, infoMock.Object);

            // Assert
            var hasInfo = workflowManager.HasInfo(FirstStepIdentifier);
            Assert.IsTrue(hasInfo);
        }

        [Test]
        [Category(TestCategory.UnitTest)]
        public void StoreInfo_DependentStep_ShouldStoreStepInfo()
        {
            // Arrange
            var infoMock = this.GetOrCreateMock<IWorkflowInfo>();

            // Act
            workflowManager.StoreInfo(SecondStepIdentifier, infoMock.Object);

            // Assert
            var hasInfo = workflowManager.HasInfo(SecondStepIdentifier);
            Assert.IsTrue(hasInfo);
        }

        [Test]
        [Category(TestCategory.UnitTest)]
        public void StoreInfo_UnknownStep_ShouldThrowException()
        {
            // Arrange
            var infoMock = this.GetOrCreateMock<IWorkflowInfo>();

            // Act & Assert
            Assert.Throws<WorkflowException>(() => workflowManager.StoreInfo("UnknownIdentifier", infoMock.Object));
        }

        [Test]
        [Category(TestCategory.UnitTest)]
        public void AccessContext_IndependentStepAllInfosAdded_ShouldCreateAndReturnContext()
        {
            // Arrange
            StoreFirstStepInfo();
            Mock<IWorkflowContext> contextMock = null;
            var contextComposerMock = CreateContextComposerMock(() => this.CreateMock(out contextMock).Object);

            SetupContextComposerProvider(FirstStepIdentifier, contextComposerMock.Object);

            // Act
            var result = workflowManager.AccessContext<IWorkflowContext>(FirstStepIdentifier);

            // Assert
            Assert.AreEqual(contextMock?.Object, result);

            contextComposerMock.Verify(composer => composer.Compose(It.IsAny<IEnumerable<IWorkflowInfo>>()), Times.Once);
        }

        [Test]
        [Category(TestCategory.UnitTest)]
        public void AccessContext_IndependentStepNoInfoAdded_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<WorkflowException>(() => workflowManager.AccessContext<IWorkflowContext>(FirstStepIdentifier));
        }

        [Test]
        [Category(TestCategory.UnitTest)]
        public void AccessContext_IndependentStepAllInfosAdded_ShouldReturnSameContextForConsecutiveCalls()
        {
            // Arrange
            StoreFirstStepInfo();

            var contextComposerMock = CreateContextComposerMock(() => this.CreateMock<IWorkflowContext>().Object);

            SetupContextComposerProvider(FirstStepIdentifier, contextComposerMock.Object);

            // Act
            var results = ExecuteConsecutively(() => workflowManager.AccessContext<IWorkflowContext>(FirstStepIdentifier), 2);

            // Assert
            BatchAssert.AssertSimple(Assert.AreSame, results);

            contextComposerMock.Verify(composer => composer.Compose(It.IsAny<IEnumerable<IWorkflowInfo>>()), Times.Once);
        }

        [Test]
        [Category(TestCategory.UnitTest)]
        public void AccessContext_IndependentStepAllInfosAdded_ShouldReturnDifferentContextsForCallsFromDifferentThreads()
        {
            // Arrange
            StoreFirstStepInfo();

            var contextComposerMock = CreateContextComposerMock(() => this.CreateMock<IWorkflowContext>().Object);

            SetupContextComposerProvider(FirstStepIdentifier, contextComposerMock.Object);

            // Act
            var results = ExecuteParallel(() => workflowManager.AccessContext<IWorkflowContext>(FirstStepIdentifier), 2);

            // Assert
            BatchAssert.AssertSimple(Assert.IsNotNull, results);
            BatchAssert.AssertExhaustive(Assert.AreNotSame, results);

            contextComposerMock.Verify(composer => composer.Compose(It.IsAny<IEnumerable<IWorkflowInfo>>()), Times.Exactly(2));
        }

        [Test]
        [Category(TestCategory.UnitTest)]
        public void AccessContext_DependentStepDependentInfoNotAdded_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<WorkflowException>(() => workflowManager.AccessContext<IWorkflowContext>(SecondStepIdentifier));
        }

        
        [Test]
        [Category(TestCategory.UnitTest)]
        public void AccessContext_DependentStepDeepDependentInfoNotAdded_ShouldThrowException()
        {
            // Arrange
            StoreSecondStepInfo();
            StoreThirdStepInfo();

            // Act & Assert
            Assert.Throws<WorkflowException>(() => workflowManager.AccessContext<IWorkflowContext>(ThirdStepIdentifier));
        }

        [Test]
        [Category(TestCategory.UnitTest)]
        public void AccessContext_DependentStepOwnInfoNotAdded_ShouldThrowException()
        {
            // Arrange
            StoreFirstStepInfo();

            // Act & Assert
            Assert.Throws<WorkflowException>(() => workflowManager.AccessContext<IWorkflowContext>(SecondStepIdentifier));
        }

        [Test]
        [Category(TestCategory.UnitTest)]
        public void AccessContext_DependentStepAllInfosAdded_ShouldCreateAndReturnContext()
        {
            // Arrange
            StoreFirstStepInfo();
            StoreSecondStepInfo();

            Mock<IWorkflowContext> contextMock = null;
            var contextComposerMock = CreateContextComposerMock(() => this.CreateMock(out contextMock).Object);

            SetupContextComposerProvider(SecondStepIdentifier, contextComposerMock.Object);

            // Act
            var result = workflowManager.AccessContext<IWorkflowContext>(SecondStepIdentifier);

            // Assert
            Assert.AreEqual(contextMock?.Object, result);

            contextComposerMock.Verify(m => m.Compose(It.IsAny<IEnumerable<IWorkflowInfo>>()), Times.Once);
        }

        [Test]
        [Category(TestCategory.UnitTest)]
        public void AccessContext_UnknownStep_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<WorkflowException>(() => workflowManager.AccessContext<IWorkflowContext>("UnknownIdentifier"));
        }

        #region Helpers

        private void StoreFirstStepInfo()
        {
            var infoMock = this.GetOrCreateMock<IWorkflowInfo>();
            workflowManager.StoreInfo(FirstStepIdentifier, infoMock.Object);
        }

        private void StoreSecondStepInfo()
        {
            var infoMock = this.GetOrCreateMock<IWorkflowInfo>();
            workflowManager.StoreInfo(SecondStepIdentifier, infoMock.Object);
        }

        private void StoreThirdStepInfo()
        {
            var infoMock = this.GetOrCreateMock<IWorkflowInfo>();
            workflowManager.StoreInfo(ThirdStepIdentifier, infoMock.Object);
        }

        private void SetupContextComposerProvider<TContext>(string identifier, IWorkflowContextComposer<TContext> contextComposer)
            where TContext : IWorkflowContext
        {
            this.GetMock<IWorkflowContextComposerProvider>()
                .Setup(m => m.Provide<TContext>(identifier))
                .Returns(contextComposer);
        }

        private Mock<IWorkflowContextComposer<TContext>> CreateContextComposerMock<TContext>(Func<TContext> contextProvider)
            where TContext : class, IWorkflowContext
        {
            return this.GetOrCreateMock<IWorkflowContextComposer<TContext>>(
                mock => mock
                    .Setup(m => m.Compose(It.IsAny<IEnumerable<IWorkflowInfo>>()))
                    .Returns(contextProvider));
        }

        #endregion
    }
}