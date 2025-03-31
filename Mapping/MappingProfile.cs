using AutoMapper;
using JobSphere.DTOs.Users;
using JobSphere.Entities;

namespace JobSphere.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, CreateUserDto>();
        }
    }
}
