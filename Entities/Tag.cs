using System.ComponentModel.DataAnnotations;

namespace JobSphere.Entities
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public ICollection<JobTag> JobTags { get; set; }
    }
}
