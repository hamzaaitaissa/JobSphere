using System.ComponentModel.DataAnnotations;

namespace JobSphere.Entities
{
    public class JobApplicationEntity
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }

        public int ApplicantId { get; set; }
        public User Applicant { get; set; }

        public string CvPath { get; set; }  

        public DateTime UploadedAt { get; set; }
    }
}
