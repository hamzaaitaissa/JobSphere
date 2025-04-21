using JobSphere.Data;
using JobSphere.Entities;
using JobSphere.Entities.JobApplication;
using JobSphere.Repositories.Jobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobSphere.Repositories.JobAppRepo
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly JobSphereContext _JobSphereContext;
        private readonly ILogger _logger;
        private readonly IJobRepository _jobRepository;

        public JobApplicationRepository(JobSphereContext jobSphereContext, ILogger logger, IJobRepository jobRepository)
        {
            _JobSphereContext = jobSphereContext;
            _logger = logger;
            _jobRepository = jobRepository;
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

        public async Task<JobApplicationEntity?> GetByApplicantAndJobIdAsync(int applicantId, int jobId)
        {
            return await _JobSphereContext.JobApplications
                .FirstOrDefaultAsync(ja => ja.ApplicantId == applicantId && ja.JobId == jobId);
        }

        public Task<JobApplicationEntity?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<JobApplicationEntity>> GetByJobIdAsync(int jobId)
        {
            throw new NotImplementedException();
        }

        public async Task<Boolean> CheckJobIsOpenAsync(int jobId)
        {
            return await _jobRepository.CheckIsOpenAsync(jobId);
        }
    }
}
