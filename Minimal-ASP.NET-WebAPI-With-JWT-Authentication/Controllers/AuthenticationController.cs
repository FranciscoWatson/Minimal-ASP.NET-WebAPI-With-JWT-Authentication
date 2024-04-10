using Microsoft.AspNetCore.Mvc;
using Minimal_ASP.NET_WebAPI_With_JWT_Authentication.Authentication;
using Minimal_ASP.NET_WebAPI_With_JWT_Authentication.Models;
using Minimal_ASP.NET_WebAPI_With_JWT_Authentication.Repositories;

namespace Minimal_ASP.NET_WebAPI_With_JWT_Authentication.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : Controller
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationController(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequestBody authenticationRequestBody)
        {
            var user = ValidateUserCredentials(authenticationRequestBody);

            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(_jwtTokenGenerator.GenerateToken(user));

        }

        private User ValidateUserCredentials(AuthenticationRequestBody authenticationRequestBody)
        {
            var user = _userRepository.Get(authenticationRequestBody.Username, authenticationRequestBody.Password).Result;
            if (user == null)
            {
                return null;
            }

            return user;

        }
    }
}
