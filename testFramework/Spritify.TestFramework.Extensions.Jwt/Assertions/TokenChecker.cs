using System;
using System.IdentityModel.Tokens.Jwt;
using Spritify.Common.Extensions;
using Spritify.TestFramework.Assertions;
using Spritify.TestFramework.Assertions.Inequality;
using Spritify.TestFramework.Assertions.Nullable;

namespace Spritify.TestFramework.Extensions.Jwt.Assertions
{
    public class TokenChecker
    {
        private JwtSecurityToken Token { get; }

        public TokenChecker(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            Token = tokenHandler.ReadJwtToken(token);

            NullableAssert.IsNotNull(Token);

            var currentTimestamp = DateTime.UtcNow.ToUnixTimestamp();
            InequalityAssert.IsGreaterOrEqual(currentTimestamp, Token.ValidFrom.ToUnixTimestamp());
            InequalityAssert.IsGreaterOrEqual(Token.ValidTo.ToUnixTimestamp(), currentTimestamp);
        }

        public TokenChecker ExpiresBeforeOrAt(DateTime expirationDate)
        {
            Assert.IsGreaterOrEqual(expirationDate, Token.ValidTo);
            return this;
        }
    }
}