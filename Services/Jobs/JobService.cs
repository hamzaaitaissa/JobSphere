using AutoMapper;
using JobSphere.DTOs.Jobs;
using JobSphere.Entities;
using JobSphere.Repositories.Jobs;

namespace JobSphere.Services.Jobs
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;

        public JobService(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Job>> GetAllJobsAsync()
        {
            var jobs = await _jobRepository.GetAllAsync();
            return jobs;
        }

        Task<Job> IJobService.CreateJobAsync(CreateJobDto createJobDto)
        {
            throw new NotImplementedException();
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
