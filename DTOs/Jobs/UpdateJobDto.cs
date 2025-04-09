using System.ComponentModel.DataAnnotations;

namespace JobSphere.DTOs.Jobs
{
    public class UpdateJobDto
    {
        [Required, MaxLength(50)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal? Salary { get; set; }
        [Required]
        public bool IsOpen { get; set; } = true;
        [Required]
        public List<string> JobTags { get; set; } = new();
    }
}
