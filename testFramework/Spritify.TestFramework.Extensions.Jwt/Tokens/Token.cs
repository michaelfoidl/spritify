namespace Spritify.TestFramework.Extensions.Jwt.Tokens
{
    public class Token
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public long IssuedAt { get; set; }
        public long NotBefore { get; set; }
        public long ExpirationTime { get; set; }
    }
}