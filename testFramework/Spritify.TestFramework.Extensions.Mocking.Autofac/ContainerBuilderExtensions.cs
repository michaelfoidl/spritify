using System;
using Autofac;
using Autofac.Builder;
using Autofac.Extras.Moq;
using Moq;
using Spritify.TestFramework.Extensions.Mocking.Interfaces;

namespace Spritify.TestFramework.Extensions.Mocking.Autofac
{
    public static class ContainerBuilderExtensions
    {
        public static IRegistrationBuilder<TMocked, SimpleActivatorData, SingleRegistrationStyle> RegisterMock<TMocked>(
            this ContainerBuilder builder,
            Action<Mock<TMocked>> mockConfiguration,
            IMockStore mockStore
        )
            where TMocked : class
        {
            var result =  SetupAndRegisterMock(builder, mockConfiguration, mockStore);
            return result;
        }

        public static IRegistrationBuilder<TMocked, SimpleActivatorData, SingleRegistrationStyle> RegisterMock<TMocked>(
            this ContainerBuilder builder,
            IMockStore mockStore
        )
            where TMocked : class
        {
            var result = SetupAndRegisterMock<TMocked>(builder, null, mockStore);
            return result;
        }

        private static IRegistrationBuilder<TMocked, SimpleActivatorData, SingleRegistrationStyle> SetupAndRegisterMock<TMocked>(
            ContainerBuilder builder,
            Action<Mock<TMocked>> mockConfiguration,
            IMockStore mockStore
        )
            where TMocked : class
        {
            var mock = MockHelper.GetOrCreateMock(mockConfiguration, mockStore);

            return builder.RegisterMock(mock);
        }
    }
}