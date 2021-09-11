using System;
using Moq;
using Spritify.TestFramework.Extensions.Mocking.Interfaces;

namespace Spritify.TestFramework.Extensions.Mocking
{
    public static class MockHelper
    {
        public static Mock<TMocked> GetMock<TMocked>(IMockStore mockStore)
            where TMocked : class
        {
            return mockStore.Get<TMocked>();
        }

        public static Mock<TMocked> GetOrCreateMock<TMocked>(Action<Mock<TMocked>> mockConfiguration, IMockStore mockStore)
            where TMocked : class
        {
            return GetOrCreateMock(mockConfiguration, Array.Empty<object>(), mockStore);
        }

        public static Mock<TMocked> GetOrCreateMock<TMocked>(Action<Mock<TMocked>> mockConfiguration, object[] arguments, IMockStore mockStore)
            where TMocked : class
        {
            var existingMock = GetMock<TMocked>(mockStore);
            if (existingMock != null)
            {
                return existingMock;
            }

            var mock = CreateMock(mockConfiguration, arguments);

            mockStore.Set(mock);

            return mock;
        }

        public static Mock<TMocked> CreateMock<TMocked>(Action<Mock<TMocked>> mockConfiguration, params object[] arguments)
            where TMocked : class
        {
            var mock = new Mock<TMocked>(MockBehavior.Strict, arguments);

            mockConfiguration?.Invoke(mock);

            if (typeof(IDisposable).IsAssignableFrom(typeof(TMocked)))
            {
                mock.As<IDisposable>().SetupDispose();
            }

            return mock;
        }
    }
}