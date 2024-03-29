using AutoMapper;
using Flipster.Modules.Users.Domain.Entities;
using Flipster.Modules.Users.Dtos;

namespace Flipster.Modules.Users.Profiles;

public class LocationProfile : Profile
{
    public LocationProfile()
    {
        CreateMap<Location, LocationDto>()
            .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Value));
    }
}