using JobSphere.Data;
using JobSphere.DTOs.Auth;
using JobSphere.DTOs.Users;
using JobSphere.Entities;
using JobSphere.ENUMS;
using JobSphere.Services;
using JobSphere.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
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
        private readonly ILogger<AuthController> _logger;
        private readonly IPasswordHasherService _passwordHasher;


        public AuthController(ILogger<AuthController> logger, IConfiguration configuration, IPasswordHasherService passwordHasher, JobSphereContext jobSphereContext, IUserService userService)
        {
            _configuration = configuration;
            _jobSphereContext = jobSphereContext;
            _userService = userService;
            _passwordHasher = passwordHasher;
            _logger = logger;

        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            // first finding user by email and password
            var user = await _jobSphereContext.Users.SingleOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null || !_passwordHasher.VerifyPassword(loginDto.Password, user.HashedPassword, user.PasswordSalt))
            {
                return Unauthorized("Invalid email or password.");
            }

            // 3. Retrieve JWT settings safely
            var jwtSettingsSection = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettingsSection.GetValue<string>("SecretKey");
            var issuer = jwtSettingsSection.GetValue<string>("Issuer");
            var audience = jwtSettingsSection.GetValue<string>("Audience");
            var expiryInHours = jwtSettingsSection.GetValue<int>("ExpiryInHours"); 

            // 4. Validate required JWT settings
            if (string.IsNullOrEmpty(secretKey) || expiryInHours <= 0)
            {
                _logger.LogError("JWT configuration is missing or invalid (SecretKey/ExpiryInHours). Expiry read: {Expiry}", expiryInHours);
                return StatusCode(StatusCodes.Status500InternalServerError, "Server configuration error during authentication.");
            }
            Debug.WriteLine($"Login - Using ExpiryInHours: {expiryInHours}");

            // 5. Create Claims
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, Enum.GetName(typeof(UserRole), user.Role))
        };

            // 6. Create Signing Credentials
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 7. Create the Token
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(expiryInHours),
                signingCredentials: creds
            );

            // 8. Serialize and Return
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { token = tokenString });
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
