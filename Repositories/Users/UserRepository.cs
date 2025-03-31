using JobSphere.Data;
using JobSphere.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobSphere.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly JobSphereContext _jobSphereContext;

        public UserRepository(JobSphereContext jobSphereContext)
        {
            _jobSphereContext = jobSphereContext;
        }
        public async Task<User> CreateAsync(User user)
        {
            _jobSphereContext.Users.Add(user);
            await _jobSphereContext.SaveChangesAsync();
            return user;
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _jobSphereContext.Users.FindAsync(id);
            if (user != null)
            {
                _jobSphereContext.Users.Remove(user);
                await _jobSphereContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = await _jobSphereContext.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _jobSphereContext.Users.FindAsync(id);
            if (user != null)
            {
                return user;
            }
            throw new KeyNotFoundException($"User with ID {id} not found.");
        }

        public async Task<User> GetByUserEmailAsync(string Email)
        {
            var user = await _jobSphereContext.Users.FindAsync($"{Email}");
            if (user != null)
            {
                return user;
            }
            throw new KeyNotFoundException($"User with email {Email} not found.");
        }

        public async Task UpdateAsync(User user)
        {
            _jobSphereContext.Users.Update(user);
            await _jobSphereContext.SaveChangesAsync();
        }
    }
}
