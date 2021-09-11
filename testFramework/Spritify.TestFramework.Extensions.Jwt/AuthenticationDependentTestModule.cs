using Autofac;
using Spritify.Common.Autofac;
using Spritify.TestFramework.Extensions.Jwt.Interfaces.Tokens;
using Spritify.TestFramework.Extensions.Jwt.Tokens;

namespace Spritify.TestFramework.Extensions.Jwt
{
    public class AuthenticationDependentTestModule : ModuleBase
    {
        public override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TokenBuilder>().As<IDefaultTokenBuilder>().As<IExtendedTokenBuilder>().InstancePerTest();
        }
    }
}
