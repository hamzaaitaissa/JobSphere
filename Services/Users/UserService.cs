using AutoMapper;
using JobSphere.DTOs.Users;
using JobSphere.Entities;
using JobSphere.ENUMS;
using JobSphere.Repositories;
using Microsoft.AspNetCore.Identity;

namespace JobSphere.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<User> CreateUserAsync(CreateUserDto createUserDto)
        {
            //TODO: chack if user already exists
            var existingUser = await _userRepository.GetByUserEmailAsync(createUserDto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("A user with the same Email is already registred");
            }
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

        public async Task<UserDto> GetUserByEmailAsync(string Email)
        {
            var userEmail = await _userRepository.GetByUserEmailAsync(Email);
            var userEmailDto = _mapper.Map<UserDto>(userEmail);
            return userEmailDto;
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;

        }

        public async Task UpdateUserAsync(UpdateUserDto updateUserDto, int id)
        {
            var userDto = _mapper.Map<User>(updateUserDto);
            await _userRepository.UpdateAsync(userDto);
        }
    }
}
