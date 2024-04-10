using Minimal_ASP.NET_WebAPI_With_JWT_Authentication.Models;

namespace Minimal_ASP.NET_WebAPI_With_JWT_Authentication.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>
        {
            new User { UserId = 1, FirstName = "John", LastName = "Doe", Username = "john.doe", Password = "password" },
            new User { UserId = 2, FirstName = "Jane", LastName = "Doe", Username = "jane.doe", Password = "password" }
        };

        public Task<User> Get(string username, string password)
        {
            return Task.FromResult(_users.FirstOrDefault(u => u.Username == username && u.Password == password));
        }
    }
}
