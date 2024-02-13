using AutoMapper;
using Flipster.Modules.Identity.Dtos;

namespace Flipster.Modules.Identity.Profiles;

public class UserProfile : Profile
{   
    public UserProfile()
    {
        CreateMap<Domain.User.Entities.User, UserDto>()
            .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar ?? string.Empty))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber ?? string.Empty))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location ?? string.Empty));
    }
}