using AutoMapper;
using Flipster.Modules.Locations.Dto;
using Flipster.Modules.Locations.Entities;

namespace Flipster.Modules.Images.Profiles;

public class LocationProfile : Profile
{
    public LocationProfile()
    {
        CreateMap<Location, LocationDto>()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => $"{src.Country}, {src.State}, {src.City}"));
    }
}