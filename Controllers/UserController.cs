using JobSphere.DTOs.Users;
using JobSphere.Entities;
using JobSphere.Services.Users;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<UserDto>> GetUserByEmailAsync([FromQuery] string Email)
        {
            return await _userService.GetUserByEmailAsync(Email);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [Authorize(Roles = "Admin", Policy = "UserOwnershipPolicy")]
        [HttpDelete]
        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok("User deleted Successfully");
        }
        
        [HttpPut("{id}")]
        [Authorize(Policy = "UserOwnershipPolicy")]
        public async Task<ActionResult> UpdateUserAsync(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            System.Diagnostics.Debug.WriteLine("Hey");
            await _userService.UpdateUserAsync(updateUserDto, id);
            return Ok("User Updated Successfully");
        }

    }
}
