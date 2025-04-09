using JobSphere.DTOs.Jobs;
using JobSphere.Entities;

namespace JobSphere.Services.Jobs
{
    public interface IJobService
    {
        Task<IEnumerable<Job>> GetAllJobsAsync();
        Task<Job> GetJobByIdAsync();
        Task<IEnumerable<Job>> GetJobsByTagsAsync(List<string> tagTitles);
        Task<Job> GetJobByNameAsync(string Name);
        Task UpdateJobAsync(int id, UpdateJobDto updateJobDto);
        Task DeleteJobAsync(int id);
        Task<Job> CreateJobAsync(CreateJobDto createJobDto);
        Task<IEnumerable<Job>> GetJobsByEmployerAsync(int employerId);
        Task ToggleJobStatusAsync(int jobId);

    }
}
