using AutoMapper;
using Flipster.Modules.Users.Domain.Entities;
using Flipster.Modules.Users.Dtos;

namespace Flipster.Modules.Users.Profiles;

internal class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location != null ? src.Location.Value : string.Empty))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber ?? string.Empty))
            .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar ?? string.Empty))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
    }
}
