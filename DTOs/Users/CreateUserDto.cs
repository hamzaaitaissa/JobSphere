using System.ComponentModel.DataAnnotations;

namespace JobSphere.DTOs.Users
{
    public record class CreateUserDto
    {
        [Required]
        [StringLength(50)]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string HashedPassword { get; set; }
    }
}
