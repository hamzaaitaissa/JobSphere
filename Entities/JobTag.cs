using System.ComponentModel.DataAnnotations;

namespace JobSphere.Entities
{
    public class JobTag
    {
        [Key]
        public int Id { get; set; }

        public int JobId { get; set; }
        public Job Job { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
