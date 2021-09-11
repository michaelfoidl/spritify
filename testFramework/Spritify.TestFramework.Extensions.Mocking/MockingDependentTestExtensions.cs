using System;
using Moq;
using Spritify.TestFramework.Extensions.Mocking.Interfaces;

namespace Spritify.TestFramework.Extensions.Mocking
{
    public static class MockingDependentTestExtensions
    {
        public static Mock<TMocked> GetMock<TMocked>(this IMockingDependentTest test)
            where TMocked : class
        {
            return MockHelper.GetMock<TMocked>(test.MockStore);
        }

        public static Mock<TMocked> GetOrCreateMock<TMocked>(this IMockingDependentTest test, params object[] arguments)
            where TMocked : class
        {
            return GetOrCreateMock<TMocked>(test, null, arguments);
        }

        public static Mock<TMocked> GetOrCreateMock<TMocked>(this IMockingDependentTest test, out Mock<TMocked> mock)
            where TMocked : class
        {
            mock = GetOrCreateMock<TMocked>(test);

            return mock;
        }

        public static Mock<TMocked> GetOrCreateMock<TMocked>(this IMockingDependentTest test, object[] arguments, out Mock<TMocked> mock)
            where TMocked : class
        {
            mock = GetOrCreateMock<TMocked>(test, arguments);

            return mock;
        }

        public static Mock<TMocked> GetOrCreateMock<TMocked>(this IMockingDependentTest test, Action<Mock<TMocked>> mockConfiguration, params object[] arguments)
            where TMocked : class
        {
            var mock = MockHelper.GetOrCreateMock(mockConfiguration, arguments, test.MockStore);

            return mock;
        }

        public static Mock<TMocked> GetOrCreateMock<TMocked>(this IMockingDependentTest test, Action<Mock<TMocked>> mockConfiguration, out Mock<TMocked> mock)
            where TMocked : class
        {
            mock = GetOrCreateMock(test, mockConfiguration);

            return mock;
        }

        public static Mock<TMocked> GetOrCreateMock<TMocked>(this IMockingDependentTest test, Action<Mock<TMocked>> mockConfiguration, object[] arguments, out Mock<TMocked> mock)
            where TMocked : class
        {
            mock = GetOrCreateMock(test, mockConfiguration, arguments);

            return mock;
        }

        public static Mock<TMocked> CreateMock<TMocked>(this IMockingDependentTest test, params object[] arguments)
            where TMocked : class
        {
            return CreateMock<TMocked>(test, null, arguments);
        }


        public static Mock<TMocked> CreateMock<TMocked>(this IMockingDependentTest test, out Mock<TMocked> mock)
            where TMocked : class
        {
            mock = CreateMock<TMocked>(test);

            return mock;
        }

        public static Mock<TMocked> CreateMock<TMocked>(this IMockingDependentTest test, object[] arguments, out Mock<TMocked> mock)
            where TMocked : class
        {
            mock = CreateMock<TMocked>(test, arguments);

            return mock;
        }

        public static Mock<TMocked> CreateMock<TMocked>(this IMockingDependentTest test, Action<Mock<TMocked>> mockConfiguration, params object[] arguments)
            where TMocked : class
        {
            var mock = MockHelper.CreateMock(mockConfiguration, arguments);

            return mock;
        }

        public static Mock<TMocked> CreateMock<TMocked>(this IMockingDependentTest test, Action<Mock<TMocked>> mockConfiguration, out Mock<TMocked> mock)
            where TMocked : class
        {
            mock = CreateMock(test, mockConfiguration);

            return mock;
        }

        public static Mock<TMocked> CreateMock<TMocked>(this IMockingDependentTest test, Action<Mock<TMocked>> mockConfiguration, object[] arguments, out Mock<TMocked> mock)
            where TMocked : class
        {
            mock = CreateMock(test, mockConfiguration, arguments);

            return mock;
        }
    }
}
