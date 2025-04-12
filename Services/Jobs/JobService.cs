using AutoMapper;
using JobSphere.DTOs.Jobs;
using JobSphere.Entities;
using JobSphere.Repositories.Jobs;
using System.Linq;
using System.Security.Claims;

namespace JobSphere.Services.Jobs
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<JobService> _logger;

        public JobService(ILogger<JobService> logger, IJobRepository jobRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Job>> GetAllJobsAsync()
        {
            var jobs = await _jobRepository.GetAllAsync();
            return jobs;
        }

        async Task<Job> IJobService.CreateJobAsync(CreateJobDto createJobDto)
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
                _logger.LogError("Could not find or parse required NameIdentifier claim for job creation.");
                throw new InvalidOperationException("User ID claim (NameIdentifier) is missing or invalid in the token.");
            }


            var job = _mapper.Map<Job>(createJobDto);
            job.EmployerId = employerId; 
            await _jobRepository.CreateAsync(job);

            return job;
        }

        async Task IJobService.DeleteJobAsync(int id)
        {
            var job = await _jobRepository.GetByIdAsync(id);
            if(job != null)
            {
                await _jobRepository.DeleteAsync(id);
            }
            
        }

        async Task<IEnumerable<Job>> IJobService.GetAllJobsAsync()
        {
            var jobs = await _jobRepository.GetAllAsync();
            return jobs;
        }

        async Task<Job> IJobService.GetJobByIdAsync(int id)
        {
            var job = await _jobRepository.GetByIdAsync(id);
            return job;
        }

        async Task<IEnumerable<Job>> IJobService.GetJobsByNameAsync(string Name)
        {
            var jobs = await _jobRepository.GetAllAsync();
            var jobsByName = jobs.Where(j => j.Title.Contains(Name, StringComparison.OrdinalIgnoreCase));
            return jobsByName;
        }

        async Task<IEnumerable<Job>> IJobService.GetJobsByEmployerAsync(int employerId)
        {
            var jobs = await _jobRepository.GetAllAsync();
            var jobsByEmployer = jobs.Where(j => j.EmployerId == employerId);
            return jobsByEmployer;
        }

        async Task<IEnumerable<Job>> IJobService.GetJobsByTagsAsync(List<string> tagTitles)
        {
            var jobs = await _jobRepository.GetAllAsync();

            var jobsByTags = jobs.Where(job =>
                job.JobTags.Any(jt =>
                    tagTitles.Contains(jt.Tag.Title, StringComparer.OrdinalIgnoreCase)
                )).ToList();

            return jobsByTags;
        }

        async Task<string> IJobService.ToggleJobStatusAsync(int jobId)
        {
            var job = await _jobRepository.GetByIdAsync(jobId);
            if (job == null) throw new KeyNotFoundException($"Job with ID {jobId} not found.");

            job.IsOpen = !job.IsOpen;
            await _jobRepository.UpdateAsync(jobId, job);

            return job.IsOpen ? "Job is now open" : "Job is now closed";
        }

        async Task IJobService.UpdateJobAsync(int id, UpdateJobDto updateJobDto)
        {
            var existingJob = await _jobRepository.GetByIdAsync(id);
            if (existingJob == null) throw new KeyNotFoundException($"Job with ID {id} not found.");

            _mapper.Map(updateJobDto, existingJob);

            existingJob.UpdatedAt = DateTime.UtcNow;

            await _jobRepository.UpdateAsync(id, existingJob);

        }
    }
}
