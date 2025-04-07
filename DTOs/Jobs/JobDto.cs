using JobSphere.Entities;
using System.ComponentModel.DataAnnotations;

namespace JobSphere.DTOs.Jobs
{
    public record class JobDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int EmployerId { get; set; }
        public decimal? Salary { get; set; }
        public bool IsOpen { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<string> JobTags { get; set; } = new();
    }
}
