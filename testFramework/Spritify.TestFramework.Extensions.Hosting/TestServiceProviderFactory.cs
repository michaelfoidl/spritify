using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Spritify.Common;

namespace Spritify.TestFramework.Extensions.Hosting
{
    public class TestServiceProviderFactory : IServiceProviderFactory<ContainerBuilder>
    {
        private readonly Action<ContainerBuilder> containerConfiguration;

        public TestServiceProviderFactory(Action<ContainerBuilder> containerConfiguration = null)
        {
            this.containerConfiguration = containerConfiguration ?? (builder => { });
        }

        public ContainerBuilder CreateBuilder(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            builder.Populate(services);

            return builder;
        }

        public IServiceProvider CreateServiceProvider(ContainerBuilder containerBuilder)
        {
            Ensure.ArgumentIsNotNull(containerBuilder, nameof(containerBuilder));

            containerConfiguration(containerBuilder);

            var container = containerBuilder.Build();

            return new AutofacServiceProvider(container);
        }
    }
}