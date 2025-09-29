using Application.DTOs.UserDto;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserForCreation user)
        {
            if (user == null) return BadRequest("User cannot be null.");
            try
            {
                var createdUserId = await _userService.CreateUserAsync(user);
                return Ok(createdUserId);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating user: {ex.Message}");
            }
        }
    }
}
