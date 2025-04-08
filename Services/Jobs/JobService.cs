using JobSphere.Entities;

namespace JobSphere.Services.Jobs
{
    public class JobService : IJobService
    {
        public Task<IEnumerable<Job>> GetAllJobsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
