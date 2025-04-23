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
        private readonly IPasswordHasherService _passwordHasher;

        public UserService(IPasswordHasherService passwordHasher, IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> CreateUserAsync(CreateUserDto createUserDto)
        {
            //TODO: chack if user already exists
            var existingUser = await _userRepository.GetByUserEmailAsync(createUserDto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("A user with the same Email is already registred");
            }
            (string passwordHash, byte[] passwordSalt) = _passwordHasher.HashPassword(createUserDto.Password);

            var user = new User
            {
                FullName = createUserDto.FullName, // Get from DTO
                Email = createUserDto.Email,
                HashedPassword = passwordHash,  // Store the hash
                PasswordSalt = passwordSalt,   // Store the salt
                Role = createUserDto.Role,   // Or get from DTO if applicable
                CreationTime = DateTime.UtcNow
            }; 
            await _userRepository.CreateAsync(user);
            return user;
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var userDto = _mapper.Map<IEnumerable<UserDto>>(users);
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
            var userExist = await _userRepository.GetByIdAsync(id);
            if (userExist == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            (string passwordHash, byte[] passwordSalt) = _passwordHasher.HashPassword(updateUserDto.Password);
            userExist = new User
            {
                FullName = updateUserDto.FullName,
                Email = updateUserDto.Email,
                HashedPassword = passwordHash,
                PasswordSalt = passwordSalt,
                Role = (UserRole)updateUserDto.Role,
                CreationTime = DateTime.UtcNow
            };
            await _userRepository.UpdateAsync(userExist);
        }
    }
}
