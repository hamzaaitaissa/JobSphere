using JobSphere.DTOs.Users;
using JobSphere.Entities;
using JobSphere.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobSphere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserByIdAsync(int id)
        {
            return await _userService.GetUserByIdAsync(id);
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<UserDto>> GetUserByEmailAsync([FromQuery] string email)
        {
            return await _userService.GetUserByEmailAsync(email);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
    }
}
