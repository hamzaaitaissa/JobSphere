using JobSphere.ENUMS;
using System.ComponentModel.DataAnnotations;

namespace JobSphere.DTOs.Users
{
    public class UpdateUserDto
    {
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        public int Role { get; set; } 
    }
}
