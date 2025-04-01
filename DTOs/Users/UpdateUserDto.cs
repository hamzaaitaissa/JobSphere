using JobSphere.ENUMS;
using System.ComponentModel.DataAnnotations;

namespace JobSphere.DTOs.Users
{
    public class UpdateUserDto
    {
        [Required]
        [StringLength(50)]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        public UserRole Role { get; set; } = UserRole.JobSeeker;
    }
}
