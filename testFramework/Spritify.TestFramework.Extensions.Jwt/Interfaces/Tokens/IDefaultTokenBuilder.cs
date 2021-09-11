using System;

namespace Spritify.TestFramework.Extensions.Jwt.Interfaces.Tokens
{
    public interface IDefaultTokenBuilder
    {
        IDefaultTokenBuilder IssuedAt(DateTime timestamp);
        IDefaultTokenBuilder InvalidBefore(DateTime timestamp);
        IDefaultTokenBuilder ExpiresAt(DateTime timestamp);
        IDefaultTokenBuilder Issuer(string issuer);
        IDefaultTokenBuilder Audience(string audience);
    }
}