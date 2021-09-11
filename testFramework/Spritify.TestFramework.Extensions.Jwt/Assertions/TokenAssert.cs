namespace Spritify.TestFramework.Extensions.Jwt.Assertions
{
    public static class TokenAssert
    {
        public static TokenChecker IsValid(string token)
        {
            return new TokenChecker(token);
        }
    }
}