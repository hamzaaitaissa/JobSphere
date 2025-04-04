using JobSphere.Data;
using JobSphere.DTOs.Auth;
using JobSphere.DTOs.Users;
using JobSphere.Entities;
using JobSphere.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JobSphere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly JobSphereContext _jobSphereContext;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, JobSphereContext jobSphereContext, IUserService userService)
        {
            _configuration = configuration;
            _jobSphereContext = jobSphereContext;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            // first finding user by email and password
            var user = await _jobSphereContext.Users.SingleOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null)
            {
                return Unauthorized("Invalid Credentials");
            }

            //now creating claims baby
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), //ts is Important for ownership
                new Claim(JwtRegisteredClaimNames.Sub, loginDto.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            // now I retrieve JWT settings from configuration
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings.GetValue<string>("SecretKey");
            var issuer = jwtSettings.GetValue<string>("Issuer");
            var audience = jwtSettings.GetValue<string>("Audience");
            var expiryInHours = jwtSettings.GetValue<int>("ExpiryInHours");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // and ofc creatin the token
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddHours(expiryInHours),
                signingCredentials: creds);

            // Returnin the token
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });

        }

        [HttpPost("signup")]
        public async Task<ActionResult<User>> RegisterUser([FromBody] CreateUserDto createUserDto)
        {
            //if the request is bad 👎🏻 didnt pass the validation rules
            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException();
            }
            try
            {
                var res = await _userService.CreateUserAsync(createUserDto);
                return Ok(res);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
