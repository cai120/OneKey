using AutoMapper;
using OneKey.Domain;
using OneKey.Entity.Models;

namespace OneKey.Api.MappingProfiles
{
    public class PasswordMappingProfile:Profile
    {
        public PasswordMappingProfile() 
        {
            CreateMap<Password, PasswordDTO>().ReverseMap();
        }
    }
}
