using AutoMapper;
using JobSphere.DTOs.Jobs;
using JobSphere.Entities;
using JobSphere.Repositories.Jobs;
using System.Security.Claims;

namespace JobSphere.Services.Jobs
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JobService(IJobRepository jobRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Job>> GetAllJobsAsync()
        {
            var jobs = await _jobRepository.GetAllAsync();
            return jobs;
        }

        async Task<Job> IJobService.CreateJobAsync(CreateJobDto createJobDto)
        {
            var employerId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            createJobDto.EmployerId = employerId;

            var job = _mapper.Map<Job>(createJobDto);
            await _jobRepository.CreateAsync(job);
            //thank god there is a middleware
            return job;
        }

        Task IJobService.DeleteJobAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Job>> IJobService.GetAllJobsAsync()
        {
            throw new NotImplementedException();
        }

        Task<Job> IJobService.GetJobByIdAsync()
        {
            throw new NotImplementedException();
        }

        Task<Job> IJobService.GetJobByNameAsync(string Name)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Job>> IJobService.GetJobsByEmployerAsync(int employerId)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Job>> IJobService.GetJobsByTagsAsync(List<string> tagTitles)
        {
            throw new NotImplementedException();
        }

        Task IJobService.ToggleJobStatusAsync(int jobId)
        {
            throw new NotImplementedException();
        }

        Task IJobService.UpdateJobAsync(int id, UpdateJobDto updateJobDto)
        {
            throw new NotImplementedException();
        }
    }
}
