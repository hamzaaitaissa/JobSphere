using JobSphere.Entities;

namespace JobSphere.Services.Jobs
{
    public interface IJobService
    {
        Task<IEnumerable<Job>> GetAllJobsAsync();
    }
}
