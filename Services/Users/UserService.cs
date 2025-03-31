using AutoMapper;
using JobSphere.DTOs.Users;
using JobSphere.Entities;
using JobSphere.Repositories;

namespace JobSphere.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<User> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);
            await _userRepository.CreateAsync(user);
            return user;
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var userDto = _mapper.Map<IEnumerable<User>>(users);
            return userDto;
        }

        public async Task<User> GetUserByEmailAsync(string Email)
        {
            var userEmail = await _userRepository.GetByUserEmailAsync(Email);
            var userEmailDto = _mapper.Map<User>(userEmail);
            return userEmailDto;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            var userDto = _mapper.Map<User>(user);
            return userDto;

        }

        public async Task UpdateUserAsync(UpdateUserDto updateUserDto, int id)
        {
            var userDto = _mapper.Map<User>(updateUserDto);
            await _userRepository.UpdateAsync(userDto);
        }
    }
}
