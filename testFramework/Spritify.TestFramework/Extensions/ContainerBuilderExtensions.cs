using Autofac;
using Autofac.Builder;

namespace Spritify.TestFramework.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static IRegistrationBuilder<TShared, SimpleActivatorData, SingleRegistrationStyle> RegisterShared<TShared>(this ContainerBuilder builder, TShared sharedInstance)
            where TShared : class
        {
            return builder.RegisterInstance(sharedInstance).SingleInstance().ExternallyOwned();
        }
    }
}