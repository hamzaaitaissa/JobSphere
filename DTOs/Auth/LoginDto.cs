using System.ComponentModel.DataAnnotations;

namespace JobSphere.DTOs.Auth
{
    public record class LoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
