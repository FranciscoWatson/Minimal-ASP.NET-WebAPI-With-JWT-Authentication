namespace Minimal_ASP.NET_WebAPI_With_JWT_Authentication.Authentication
{
    public class JwtAuthenticationConfig
    {
        public string SecretKey { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int TokenExpiryInMinutes { get; set; }

    }
}
