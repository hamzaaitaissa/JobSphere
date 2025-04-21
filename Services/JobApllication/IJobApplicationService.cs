using JobSphere.DTOs.JobApplication;
using JobSphere.Entities;

namespace JobSphere.Services.JobApllication
{
    public interface IJobApplicationService
    {
        Task<JobApplicationEntity> ApplyJobAsync(CreateJobApplicationDto createJobApplicationDto);
    }
}
