namespace JobSphere.DTOs.JobApplication
{
    public class CreateJobApplication
    {
        public int JobId { get; set; }
        public int ApplicantId { get; set; }
        public string CvPath { get; set; }
    }
}
