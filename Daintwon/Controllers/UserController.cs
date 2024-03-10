using Daintwon.Models.DTOs;
using Daintwon.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Daintwon.Services.Implementations.UserService;

namespace Daintwon.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [Authorize(Policy = "ClientOnly")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var UserDTO = _userService.GetAllUsers();
                return Ok(UserDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                var userDTO = _userService.GetUserById(id);
                return Ok(userDTO);
            }
            catch (UserServiceException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "internal server error: " + ex.Message);
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] UserRegisterDTO user)
        {
            try
            {
                var newUser = _userService.CreateUser(user);
                return Created("", newUser);
            }
            catch (UserServiceException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "internal server error: " + ex.Message);
            }
        }
        [HttpGet("current")]
        public IActionResult GetCurrent()
        {
            try
            {
                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
                if (email == string.Empty)
                {
                    return StatusCode(403, "invalid session");
                }

                UserDTO result = _userService.GetCurrentUser(email);

                if (result == null)
                {
                    return StatusCode(404, "invalid user");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
