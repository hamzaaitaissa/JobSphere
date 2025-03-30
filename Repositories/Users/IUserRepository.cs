using JobSphere.Entities;

namespace JobSphere.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByUserEmailAsync(string Email);
        Task<IEnumerable<User>> GetAllAsync();
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);
    }
}
