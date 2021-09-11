using System;

namespace Spritify.TestFramework.Assertions
{
    public interface IAssertPreset<in TExpected, in TActual>
    {
        void Execute(TExpected expected, TActual actual);
    }

    public abstract class AssertPreset<TExpected, TActual>
    {
        private Action<TExpected, TActual> Action { get; }

        protected AssertPreset(Action<TExpected, TActual> action)
        {
            Action = action;
        }

        public void Execute(TExpected expected, TActual actual)
        {
            Action(expected, actual);
        }
    }
}