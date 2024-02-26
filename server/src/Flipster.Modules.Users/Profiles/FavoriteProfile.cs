using AutoMapper;
using Flipster.Modules.Users.Domain.Entities;
using Flipster.Modules.Users.Dtos;

namespace Flipster.Modules.Users.Profiles;

internal class FavoriteProfile : Profile
{
    public FavoriteProfile()
    {
        CreateMap<Favorite, ContractFavoriteDto>();
    }
}
