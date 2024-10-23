using Minimal_ASP.NET_WebAPI_With_JWT_Authentication.Models;
using System.Security.Cryptography;
using System.Text;

namespace Minimal_ASP.NET_WebAPI_With_JWT_Authentication.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>
        {
            new User { UserId = 1, FirstName = "John", LastName = "Doe", Username = "john.doe", Password = HashPassword("password") },
            new User { UserId = 2, FirstName = "Jane", LastName = "Doe", Username = "jane.doe", Password = HashPassword("password") }
        };

        public async Task<User> Get(string username, string password)
        {
            var hashedPassword = HashPassword(password);
            return await Task.FromResult(_users.FirstOrDefault(u => u.Username == username && u.Password == hashedPassword));
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
        }

    }
}
