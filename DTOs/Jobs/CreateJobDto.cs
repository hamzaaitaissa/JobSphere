using System.ComponentModel.DataAnnotations;

namespace JobSphere.DTOs.Jobs
{
    public class CreateJobDto
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
        public List<int> JobTags { get; set; } = new();
    }
}
