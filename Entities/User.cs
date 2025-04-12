using JobSphere.ENUMS;
using System.ComponentModel.DataAnnotations;

namespace JobSphere.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public byte[] PasswordSalt { get; set; }
        public UserRole Role { get; set; } = UserRole.JobSeeker;
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
        
    }
}
