﻿using AutoMapper;
using JobSphere.DTOs.Users;
using JobSphere.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace JobSphere.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>().
                ForMember(dest => dest.HashedPassword, opt => opt.MapFrom(src => HashPassword(src.Password)));
            CreateMap<UpdateUserDto, User>().
                ForMember(dest => dest.HashedPassword, opt => opt.MapFrom(src => HashPassword(src.Password)));
        }
        private static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return null;

            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32
            ));

            return hashed;
        }
    }
}
