using AutoMapper;
using Flipster.Modules.Identity.Domain.User.Entities;
using Flipster.Modules.Identity.Dtos;

namespace Flipster.Modules.Identity.Profiles;

public class UserProfile : Profile
{   
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar ?? string.Empty))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location ?? string.Empty));
    }
}