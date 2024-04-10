using Minimal_ASP.NET_WebAPI_With_JWT_Authentication.Models;

namespace Minimal_ASP.NET_WebAPI_With_JWT_Authentication.Repositories
{
    public interface IUserRepository
    {
        Task<User> Get(string username, string password);
    }
}
