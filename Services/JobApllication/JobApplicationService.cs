using AutoMapper;
using JobSphere.DTOs.JobApplication;
using JobSphere.Entities;
using JobSphere.Entities.JobApplication;
using System.Security.Claims;

namespace JobSphere.Services.JobApllication
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JobApplicationService(IHttpContextAccessor httpContextAccessor, IJobApplicationRepository jobApplicationRepository, IMapper mapper)
        {
            _jobApplicationRepository = jobApplicationRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<JobApplicationEntity> ApplyJobAsync(CreateJobApplicationDto createJobApplicationDto)
        {
            var userPrincipal = _httpContextAccessor.HttpContext?.User;
            if (userPrincipal == null)
            {
                // This shouldn't happen if [Authorize] is working, but good to check
                throw new InvalidOperationException("HttpContext or User principal not available.");
            }
            var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var employerId))
            {
                // Log this critical error - indicates a problem with the token or claim setup
                throw new InvalidOperationException("User ID claim (NameIdentifier) is missing or invalid in the token.");
            }

            //check if applicant already applied to this job
            var alreadyApplied = await _jobApplicationRepository.GetByApplicantAndJobIdAsync(employerId, createJobApplicationDto.JobId);
            if (alreadyApplied != null)
            {
                throw new Exception("You already applied to this job offer");
            }

            //check job status
            var IsJobOpen = await _jobApplicationRepository.CheckJobIsOpenAsync(createJobApplicationDto.JobId);
            if (!IsJobOpen) throw new Exception("You cannot apply to this Job.");
            //if al good save jobApplication
            var jobApplicationMapped = _mapper.Map<JobApplicationEntity>(createJobApplicationDto);
            jobApplicationMapped.ApplicantId = employerId;
            var jobApplication = await _jobApplicationRepository.ApplyAsync(jobApplicationMapped);
            return jobApplication;
            
        }
    }
}
