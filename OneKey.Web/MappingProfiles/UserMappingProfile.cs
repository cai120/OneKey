using AutoMapper;
using OneKey.Web.ViewModels.User;
using OneKey.Domain.Models;

namespace OneKey.Web.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<RegisterUserViewModel, RegisterUserDTO>().ReverseMap();
        CreateMap<LoginUserViewModel, LoginUserDTO>().ReverseMap();
        CreateMap<UserViewModel, UserDTO>().ReverseMap();
    }
}

