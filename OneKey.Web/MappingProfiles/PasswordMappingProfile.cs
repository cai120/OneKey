using AutoMapper;
using OneKey.Domain;
using OneKey.Web.ViewModels;

namespace OneKey.Web.MappingProfiles
{
    public class PasswordMappingProfile:Profile
    {
        public PasswordMappingProfile()
        {
            CreateMap<PasswordViewModel, PasswordDTO>().ReverseMap();
        }
    }
}
