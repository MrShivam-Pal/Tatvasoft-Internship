using Authentication.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly List<User> _users;

        public AuthController(IConfiguration config)
        {
            _config = config;
            _users = new List<User>
            {
                new User { Username = "admin", Password = "password", Role = "Admin" },
                new User { Username = "user", Password = "password", Role = "User" }
            };
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            IActionResult response = Unauthorized();

            var user = AuthenticateUser(login.Username, login.Password);

            if (user != null)
            {
                var tokenString = CreateJwt(user.Username, user.Role);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private User AuthenticateUser(string username, string password)
        {
            return _users.SingleOrDefault(u => u.Username == username && u.Password == password);
        }

        private string CreateJwt(string name, string role)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, role)
            });
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddHours(4),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}

