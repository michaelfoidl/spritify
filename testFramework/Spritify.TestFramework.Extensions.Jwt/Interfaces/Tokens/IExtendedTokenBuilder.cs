using Spritify.TestFramework.Extensions.Jwt.Tokens;

namespace Spritify.TestFramework.Extensions.Jwt.Interfaces.Tokens
{
    public interface IExtendedTokenBuilder : IDefaultTokenBuilder
    {
        bool IsConfigured { get; }
        Token Build();
    }
}