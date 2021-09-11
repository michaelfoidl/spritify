using System;
using Spritify.TestFramework.Interfaces.Lifecycle;

namespace Spritify.TestFramework.Lifecycle
{
    public class DefaultLifecycleContext<TTestContext> : ILifecycleContext<TTestContext>
    {
        public string TestId { get; set; }
        public TTestContext Context { get; set; }

        public object CloneTestRunProperties()
        {
            return Clone(SetClonedTestRunProperties);
        }

        public object CloneTestClassProperties()
        {
            return Clone(SetClonedTestClassProperties);
        }

        protected virtual object Clone(Action<object> setClonedProperties)
        {
            var clone = new DefaultLifecycleContext<TTestContext>();
            setClonedProperties(clone);

            return clone;
        }

        protected virtual void SetClonedTestRunProperties(object clone)
        {
            // override if necessary
        }

        protected virtual void SetClonedTestClassProperties(object clone)
        {
            SetClonedTestRunProperties(clone);
            // override if necessary
        }
    }
}