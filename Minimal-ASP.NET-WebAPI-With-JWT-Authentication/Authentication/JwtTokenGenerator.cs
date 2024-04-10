using Microsoft.IdentityModel.Tokens;
using Minimal_ASP.NET_WebAPI_With_JWT_Authentication.Models;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;


namespace Minimal_ASP.NET_WebAPI_With_JWT_Authentication.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtAuthenticationConfig _jwtAuthenticationConfig;

        public JwtTokenGenerator(JwtAuthenticationConfig jwtAuthenticationConfig)
        {
            _jwtAuthenticationConfig = jwtAuthenticationConfig;
        }
        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(_jwtAuthenticationConfig.SecretKey));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForTokens = new List<Claim>
            {
                new Claim("sub", user.UserId.ToString()),
                new Claim("given_name", user.FirstName),
                new Claim("family_name", user.LastName)
            };

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtAuthenticationConfig.Issuer,
                audience: _jwtAuthenticationConfig.Audience,
                claims: claimsForTokens,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(_jwtAuthenticationConfig.TokenExpiryInMinutes),
                signingCredentials: signingCredentials
                );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return token;
        }

        public bool ValidateToken(string token)
        {
            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(_jwtAuthenticationConfig.SecretKey));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtAuthenticationConfig.Issuer,
                ValidAudience = _jwtAuthenticationConfig.Audience,
                IssuerSigningKey = securityKey
            };
          

            var tokenHandler = new JwtSecurityTokenHandler();
            
            try
            {
                tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
