using AutoMapper;
using Flipster.Modules.Identity.Application.Dtos;
using Flipster.Modules.Identity.Domain.User.Entities;

namespace Flipster.Modules.Identity.Application.Profiles;

public class UserProfile : Profile
{
    protected internal UserProfile()
    {
        CreateMap<User, UserDto>();
    }
}