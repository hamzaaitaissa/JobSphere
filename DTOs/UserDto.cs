using System.ComponentModel.DataAnnotations;

namespace JobSphere.DTOs
{
    public class UserDto
    {
        [Key]
        public Guid Id { get; set; }
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
