using System;
using Moq;

namespace Spritify.TestFramework.Extensions.Mocking.Interfaces
{
    public interface IMockStore
    {
        Mock<TMocked> Get<TMocked>()
            where TMocked : class;

        Mock Get(Type type);

        void Set<TMocked>(Mock<TMocked> mock)
            where TMocked : class;
    }
}