namespace JobSphere.Entities.Profile
{
    public class Profile
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public string About { get; set; }
        public string Skills { get; set; }       // you could also make Skills its own list/entity
        public string Languages { get; set; }
        public string Hobbies { get; set; }
        public string PhotoUrl { get; set; }

        // navigation properties
        public ICollection<Experience> Experiences { get; set; } = new List<Experience>();
        public ICollection<Education> Educations { get; set; } = new List<Education>();
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
