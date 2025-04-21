using JobSphere.Data;
using JobSphere.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobSphere.Repositories.Jobs
{
    public class JobRepository : IJobRepository
    {
        public readonly JobSphereContext _jobSphereContext;

        public JobRepository(JobSphereContext jobSphereContext)
        {
            _jobSphereContext = jobSphereContext;
        }

        public async Task<Job> CreateAsync(Job job)
        {
            _jobSphereContext.Add(job);
            await _jobSphereContext.SaveChangesAsync();
            return job;
        }

        public async Task DeleteAsync(int id)
        {
            var job = await _jobSphereContext.Jobs.FindAsync(id);
            if (job != null)
            {
                _jobSphereContext.Remove(job);
                await _jobSphereContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("User not found.");
            }

        }

        public async Task<IEnumerable<Job>> GetAllAsync()
        {
            var jobs = await _jobSphereContext.Jobs
            .Include(j => j.JobTags)
            .ThenInclude(jt => jt.Tag)
            .ToListAsync(); ;
            return jobs;
        }

        public async Task<Job> GetByIdAsync(int id)
        {
            var job = await _jobSphereContext.Jobs.FindAsync(id);
            if (job != null)
            {
                return job;
            }
            else
            {
                throw new KeyNotFoundException("User not Found.");
            }
        }

        public Task UpdateAsync(int id, Job job)
        {
            throw new NotImplementedException();
        }
        public async Task<Boolean> CheckIsOpenAsync(int id)
        {
            var job = await _jobSphereContext.Jobs.FirstOrDefaultAsync(j => j.Id == id);
            return job.IsOpen;
        }
    }
}
