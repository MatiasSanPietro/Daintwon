using Daintwon.Models;
using Daintwon.Models.DTOs;
using Daintwon.Repositories.Interfaces;
using Daintwon.Utils.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Daintwon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUserRepository userRepository, IPasswordHasher passwordHasher, IConfiguration config) : ControllerBase
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IConfiguration _config = config;

        [HttpPost("login")]
        public async Task<ActionResult> LoginJWT([FromBody] UserLoginDTO userLoginDTO)
        {
            try
            {
                User user = _userRepository.FindByEmail(userLoginDTO.Email);

                if (user == null || !_passwordHasher.VerifyPassword(userLoginDTO.Password, user.Password, user.Salt))
                    return StatusCode(401, "Datos incorrectos");

                var claims = new List<Claim>
                {
                    new("UserId", user.Id.ToString()),
                    new(ClaimTypes.Name, user.Email),
                    new(ClaimTypes.Role, "UserRole"),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var securityToken = new JwtSecurityToken(
                    issuer: _config["JWT:Issuer"],
                    audience: _config["JWT:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: creds
                );

                string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
