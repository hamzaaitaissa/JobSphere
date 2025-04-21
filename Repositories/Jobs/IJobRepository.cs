using JobSphere.Entities;

namespace JobSphere.Repositories.Jobs
{
    public interface IJobRepository
    {
        Task<Job> CreateAsync(Job job);
        Task<IEnumerable<Job>> GetAllAsync();
        Task<Job> GetByIdAsync(int id);
        Task UpdateAsync(int id, Job job);
        Task DeleteAsync(int id);
        Task<Boolean> CheckIsOpenAsync(int id);  
    }
}
