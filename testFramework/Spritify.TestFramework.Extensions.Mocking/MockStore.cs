using System;
using System.Collections.Generic;
using Moq;
using Spritify.TestFramework.Extensions.Mocking.Interfaces;

namespace Spritify.TestFramework.Extensions.Mocking
{
    public class MockStore : IMockStore
    {
        private readonly Dictionary<Type, Mock> mocks;

        public MockStore()
        {
            mocks = new Dictionary<Type, Mock>();
        }

        public Mock<TMocked> Get<TMocked>()
            where TMocked : class
        {
            return Get(typeof(TMocked)) as Mock<TMocked>;
        }

        public Mock Get(Type type)
        {
            mocks.TryGetValue(type, out var mock);
            return mock;
        }

        public void Set<TMocked>(Mock<TMocked> mock)
            where TMocked : class
        {
            mocks.Add(typeof(TMocked), mock);
        }
    }
}