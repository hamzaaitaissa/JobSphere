using JobSphere.Entities;

namespace JobSphere.Entities.JobApplication
{
    public interface IJobApplicationRepository
    {
        Task<JobApplicationEntity> ApplyAsync(JobApplicationEntity jobApplicationEntity);
        Task<JobApplicationEntity?> GetByIdAsync(int id);
        Task<IEnumerable<JobApplicationEntity>> GetByApplicantIdAsync(int applicantId);
        Task<IEnumerable<JobApplicationEntity>> GetByJobIdAsync(int jobId);
        Task<IEnumerable<JobApplicationEntity>> GetAllAsync();
        Task<bool> DeleteAsync(int id);
    }
}
