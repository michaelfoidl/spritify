using System;
using System.Linq;
using Spritify.TestFramework;
using Spritify.TestFramework.Assertions;
using Spritify.TestFramework.Assertions.Collection;
using Spritify.TestFramework.Attributes;
using Spritify.Workflow.Internal;

namespace Spritify.Workflow.Test
{
    [TestClass]
    public class WorkflowDefinitionBuilderTests : UnmanagedTest
    {
        private WorkflowDefinitionBuilder workflowDefinitionBuilder;

        private const string StepIdentifier = "MyStep";
        private const string DependentStep1Identifier = "MyDependentStep1";
        private const string DependentStep2Identifier = "MyDependentStep2";

        public override void SetupTest()
        {
            base.SetupTest();

            workflowDefinitionBuilder = new WorkflowDefinitionBuilder();
        }

        [Test]
        [Category(TestCategory.UnitTest)]
        public void AddStep_ShouldAddStepToWorkflowDefinition()
        {
            // Act
            workflowDefinitionBuilder.AddStep(step => step.WithIdentifier(StepIdentifier));
            var definition = workflowDefinitionBuilder.Build();

            // Assert
            CollectionAssert.IsOfLength(definition.Steps, 1);
        }

        [Test]
        [Category(TestCategory.UnitTest)]
        public void AddStep_WithIdentifier_ShouldAddStepToWorkflowDefinition()
        {
            // Act
            workflowDefinitionBuilder.AddStep(step => step.WithIdentifier(StepIdentifier));
            var definition = workflowDefinitionBuilder.Build();

            // Assert
            var firstStep = definition.Steps.FirstOrDefault();
            Assert.IsNotNull(firstStep);
            Assert.AreEqual(firstStep?.Identifier, StepIdentifier);
        }

        [Test]
        [Category(TestCategory.UnitTest)]
        public void AddStep_WithoutIdentifier_ShouldFail()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => workflowDefinitionBuilder.AddStep(step => { }));
        }

        
        [Test]
        [Category(TestCategory.UnitTest)]
        public void AddStep_EmptyIdentifier_ShouldFail()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => workflowDefinitionBuilder.AddStep(step => step.WithIdentifier(string.Empty)));
        }

        [Test]
        [Category(TestCategory.UnitTest)]
        public void AddStep_WhitespaceIdentifier_ShouldFail()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => workflowDefinitionBuilder.AddStep(step => step.WithIdentifier("  ")));
        }

        [Test]
        [Category(TestCategory.UnitTest)]
        public void AddStep_WithDependentStep_ShouldAddStepToWorkflowDefinition()
        {
            // Act
            workflowDefinitionBuilder.AddStep(step => step
                .WithIdentifier(StepIdentifier)
                .DependentOn(DependentStep1Identifier));
            var definition = workflowDefinitionBuilder.Build();

            // Assert
            var firstStep = definition.Steps.FirstOrDefault();
            Assert.IsNotNull(firstStep);
            CollectionAssert.IsOfLength(firstStep?.DependentOn, 1);
            Assert.AreEqual(firstStep?.DependentOn.First(), DependentStep1Identifier);
        }

        [Test]
        [Category(TestCategory.UnitTest)]
        public void AddStep_WithMultipleDependentSteps_ShouldAddStepToWorkflowDefinition()
        {
            // Act
            workflowDefinitionBuilder.AddStep(step => step
                .WithIdentifier(StepIdentifier)
                .DependentOn(DependentStep1Identifier)
                .DependentOn(DependentStep2Identifier));
            var definition = workflowDefinitionBuilder.Build();

            // Assert
            var firstStep = definition.Steps.FirstOrDefault();
            Assert.IsNotNull(firstStep);
            CollectionAssert.IsOfLength(firstStep?.DependentOn, 2);
            CollectionAssert.Contains(firstStep?.DependentOn, DependentStep1Identifier);
            CollectionAssert.Contains(firstStep?.DependentOn, DependentStep2Identifier);
        }
    }
}
