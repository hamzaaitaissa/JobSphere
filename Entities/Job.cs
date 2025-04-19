using System.ComponentModel.DataAnnotations;

namespace JobSphere.Entities
{
    public class Job
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        // Foreign Key for the User(Employer role)
        public int EmployerId { get; set; }
        public User Employer { get; set; }
        public decimal? Salary { get; set; }  
        public bool IsOpen { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Tags soon RS -> (Many-to-Many)
        public ICollection<JobTag> JobTags { get; set; }
        public ICollection<JobApplicationEntity> JobApplications { get; set; }
    }
}
