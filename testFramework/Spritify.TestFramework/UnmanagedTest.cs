using System;
using System.Linq;
using System.Threading;
using Spritify.TestFramework.Attributes;

namespace Spritify.TestFramework
{
    public abstract class UnmanagedTest
    {
        [OneTimeSetup]
        public virtual void SetupTestClass()
        {
            // override if necessary
        }

        [Setup]
        public virtual void SetupTest()
        {
            // override if necessary
        }

        [Teardown]
        public virtual void CleanupTest()
        {
            // override if necessary
        }

        [OneTimeTeardown]
        public virtual void CleanupTestClass()
        {
            // override if necessary
        }

        protected void ExecuteConsecutively(Action executionFunction, int executionCount)
        {
            ValidateExecutionCount(executionCount);

            for (int i = 0; i < executionCount; i++)
            {
                executionFunction();
            }
        }

        protected TResult[] ExecuteConsecutively<TResult>(Func<TResult> executionFunction, int executionCount)
        {
            ValidateExecutionCount(executionCount);

            var results = new TResult[executionCount];

            for (int i = 0; i < executionCount; i++)
            {
                results[i] = executionFunction();
            }

            return results;
        }

        protected TResult[] ExecuteParallel<TResult>(Func<TResult> executionFunction, int executionCount)
        {
            ValidateExecutionCount(executionCount);

            var threads = new Thread[executionCount];
            var exceptions = new Exception[executionCount];
            var results = new TResult[executionCount];

            for (int i = 0; i < executionCount; i++)
            {
                var currentExecutionCount = i;
                threads[i] = new Thread(() =>
                {
                    // catch thrown exceptions to prevent the test execution engine from crashing
                    // (somehow, NUnit cannot handle exceptions in threads other than the main thread).
                    var (exception, result) = ExecuteSafe(executionFunction);
                    exceptions[currentExecutionCount] = exception;
                    results[currentExecutionCount] = result;
                });
            }

            foreach (var thread in threads)
            {
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            // if any exceptions were detected, throw them on the main thread
            var occurredExceptions = exceptions.Where(e => e != null).ToArray();
            if (occurredExceptions.Any())
            {
                throw new AggregateException(occurredExceptions);
            }

            return results;
        }

        /// <summary>
        /// Catches thrown exceptions and returns them.
        /// </summary>
        private static Tuple<Exception, TResult> ExecuteSafe<TResult>(Func<TResult> executionFunction)
        {
            try
            {
                var result = executionFunction();
                return new Tuple<Exception, TResult>(null, result);
            }
            catch (Exception e)
            {
                return new Tuple<Exception, TResult>(e, default);
            }
        }

        private static void ValidateExecutionCount(int executionCount)
        {
            if (executionCount < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(executionCount), "Execution count must be greater than or equal to 1.");
            }
        }
    }
}