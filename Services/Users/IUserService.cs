using JobSphere.DTOs.Users;
using JobSphere.Entities;

namespace JobSphere.Services.Users
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> GetUserByEmailAsync(string Email);
        Task<User> CreateUserAsync(CreateUserDto createUserDto);
        Task UpdateUserAsync();
        Task DeleteUserAsync();
    }
}
