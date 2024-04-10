using Minimal_ASP.NET_WebAPI_With_JWT_Authentication.Models;

namespace Minimal_ASP.NET_WebAPI_With_JWT_Authentication.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
