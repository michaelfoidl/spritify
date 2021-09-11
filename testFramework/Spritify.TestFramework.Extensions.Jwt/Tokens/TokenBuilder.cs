using System;
using Spritify.Common.Extensions;
using Spritify.TestFramework.Extensions.Jwt.Interfaces.Tokens;

namespace Spritify.TestFramework.Extensions.Jwt.Tokens
{
    public class TokenBuilder : IExtendedTokenBuilder
    {
        private Token token;

        private Token Token => token ??= CreateDefaultToken();

        public bool IsConfigured => token != null;

        public IDefaultTokenBuilder IssuedAt(DateTime timestamp)
        {
            Token.IssuedAt = timestamp.ToUnixTimestamp();
            return this;
        }

        public IDefaultTokenBuilder InvalidBefore(DateTime timestamp)
        {
            Token.NotBefore = timestamp.ToUnixTimestamp();
            return this;
        }

        public IDefaultTokenBuilder ExpiresAt(DateTime timestamp)
        {
            Token.ExpirationTime = timestamp.ToUnixTimestamp();
            return this;
        }

        public IDefaultTokenBuilder Issuer(string issuer)
        {
            Token.Issuer = issuer;
            return this;
        }

        public IDefaultTokenBuilder Audience(string audience)
        {
            Token.Audience = audience;
            return this;
        }

        public Token Build()
        {
            return Token;
        }

        private static Token CreateDefaultToken()
        {
            var defaultToken = new Token
            {
                Issuer = "http://localhost/",
                Audience = "http://localhost/",
                IssuedAt = DateTime.UtcNow.ToUnixTimestamp(),
                NotBefore = DateTime.UtcNow.ToUnixTimestamp(),
                ExpirationTime = DateTime.UtcNow.AddHours(1).ToUnixTimestamp()
            };

            return defaultToken;
        }
    }
}