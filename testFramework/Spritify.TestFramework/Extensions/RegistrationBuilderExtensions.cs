using Autofac.Builder;

namespace Spritify.TestFramework.Extensions
{
    public static class RegistrationBuilderExtensions
    {
        public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> InstancePerTest<TLimit, TActivatorData, TRegistrationStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> builder)
        {
            return builder.InstancePerMatchingLifetimeScope(TestConstants.TestScopeIdentifier);
        }

        public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> InstancePerTestOperation<TLimit, TActivatorData, TRegistrationStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> builder)
        {
            return builder.InstancePerLifetimeScope();
        }
    }
}
