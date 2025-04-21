using JobSphere.Data;
using JobSphere.Entities;
using JobSphere.Entities.JobApplication;

namespace JobSphere.Repositories.JobAppRepo
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly JobSphereContext _JobSphereContext;
        private readonly ILogger _logger;

        public JobApplicationRepository(JobSphereContext jobSphereContext, ILogger logger)
        {
            _JobSphereContext = jobSphereContext;
            _logger = logger;
        }

        public async Task<JobApplicationEntity> ApplyAsync(JobApplicationEntity jobApplicationEntity)
        {
            await _JobSphereContext.JobApplications.AddAsync(jobApplicationEntity);
            await _JobSphereContext.SaveChangesAsync();
            return jobApplicationEntity;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<JobApplicationEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<JobApplicationEntity>> GetByApplicantIdAsync(int applicantId)
        {
            throw new NotImplementedException();
        }

        public Task<JobApplicationEntity?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<JobApplicationEntity>> GetByJobIdAsync(int jobId)
        {
            throw new NotImplementedException();
        }
    }
}
