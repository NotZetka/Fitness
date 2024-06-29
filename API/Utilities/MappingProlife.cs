using API.Data.Dtos;
using API.Database;
using AutoMapper;

namespace API.Utilities
{
    public class MappingProlife : Profile
    {
        public MappingProlife()
        {
            CreateMap<AppUser, UserDto>();
        }
    }
}
