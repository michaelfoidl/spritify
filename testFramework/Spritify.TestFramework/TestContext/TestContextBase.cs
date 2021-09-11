using System;
using Autofac;

namespace Spritify.TestFramework.TestContext
{
    public abstract class TestContextBase
    {
        private bool isInitialized;

        protected ILifetimeScope Scope { get; set; }

        protected void Initialize(ILifetimeScope scope, bool complete)
        {
            Scope = scope;

            if (complete)
            {
                CompleteInitialization();
            }
        }

        protected T EnsureInitialized<T>(Func<T> operation)
        {
            if (!isInitialized)
            {
                throw new InvalidOperationException("Context not initialized: Call Initialize() first.");
            }

            return operation();
        }

        protected void CompleteInitialization()
        {
            isInitialized = true;
        }
    }
}