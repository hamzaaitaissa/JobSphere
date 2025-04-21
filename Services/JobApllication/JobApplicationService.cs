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

        public async Task<JobApplicationEntity> ApplyJobAsync(CreateJobApplicationDto createJobApplicationDto)
        {
            //check if applicant already applied to this job
            var alreadyApplied = await _jobApplicationRepository.GetByApplicantAndJobIdAsync(createJobApplicationDto.ApplicantId, createJobApplicationDto.JobId);
            if(alreadyApplied != null)
            {
                throw new Exception("You already applied to this job offer");
            }
            //check job status
            var IsJobOpen = await _jobApplicationRepository.CheckJobIsOpenAsync(createJobApplicationDto.JobId);
            if (!IsJobOpen) throw new Exception("You cannot apply to this Job.");
            //if al good save jobApplication
            var jobApplicationMapped = _mapper.Map<JobApplicationEntity>(createJobApplicationDto);
            var jobApplication = await _jobApplicationRepository.ApplyAsync(jobApplicationMapped);
            return jobApplication;
            
        }
    }
}
