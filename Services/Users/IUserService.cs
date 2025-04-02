using JobSphere.DTOs.Users;
using JobSphere.Entities;

namespace JobSphere.Services.Users
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto> GetUserByEmailAsync(string Email);
        Task<User> CreateUserAsync(CreateUserDto createUserDto);
        Task UpdateUserAsync(UpdateUserDto updateUserDto, int id);
        Task DeleteUserAsync(int id);
    }
}
