namespace JobSphere.Entities.Profile
{
    public class Experience
    {
        public Guid Id { get; set; }
        public Guid ProfileId { get; set; }  //foreign key for the profile
        public string CompanyName { get; set; }
        public string RoleTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } 
        public string Location { get; set; }
        public string Description { get; set; }

        public Profile Profile { get; set; }
    }
}
