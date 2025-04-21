using AutoMapper;
using JobSphere.DTOs.JobApplication;
using JobSphere.Entities;
using JobSphere.Entities.JobApplication;

namespace JobSphere.Services.JobApllication
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IMapper _mapper;

        public JobApplicationService(IJobApplicationRepository jobApplicationRepository, IMapper mapper)
        {
            _jobApplicationRepository = jobApplicationRepository;
            _mapper = mapper;
        }

        public Task<JobApplicationEntity> ApplyJobAsync(CreateJobApplicationDto createJobApplicationDto)
        {
            
        }
    }
}
