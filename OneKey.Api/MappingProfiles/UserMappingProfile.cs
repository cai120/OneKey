using AutoMapper;
using OneKey.Shared.Models;
using OneKey.Domain.Models;
using OneKey.Entity.Models;

namespace OneKey.Api.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDTO>().ReverseMap();

        CreateMap<RegisterUserDTO, User>();
        CreateMap<LoginUserDTO, User>();
    }
}

