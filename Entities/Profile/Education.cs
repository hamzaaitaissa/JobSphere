namespace JobSphere.Entities.Profile
{
    public class Education
    {
        public Guid Id { get; set; }
        public Guid ProfileId { get; set; }
        public string Institution { get; set; }
        public string Degree { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Location { get; set; }
        public string Details { get; set; }

        public Profile Profile { get; set; }
    }
}
