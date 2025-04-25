namespace JobSphere.Entities.Profile
{
    public class Project
    {
        public Guid Id { get; set; }
        public Guid ProfileId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Url { get; set; }

        public Profile Profile { get; set; }
    }
}
