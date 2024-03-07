using Daintwon.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Daintwon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

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
    }
}
