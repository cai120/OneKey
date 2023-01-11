using AutoMapper;
using OneKey.Shared.Models;

namespace OneKey.Api.MappingProfiles;

public class UtilitiesMappingProfile : Profile
{
	public UtilitiesMappingProfile()
	{
		CreateMap(typeof(Result<>), typeof(Result<>));
	}
}
