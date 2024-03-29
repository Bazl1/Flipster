﻿using AutoMapper;
using Flipster.Modules.Users.Domain.Repositories;
using Flipster.Modules.Users.Dtos;
using Flispter.Shared.Contracts.Users;
using Flispter.Shared.Contracts.Users.Dtos;

namespace Flipster.Modules.Users.Contracts;

internal class UsersModule(
    IUserRepository _userRepository,
    IFavoriteRepository _favoriteRepository,
    IMapper _mapper) : IUsersModule
{
    public IUserDto? GetUserById(string userId)
    {
        return _mapper.Map<UserDto>(_userRepository.GetById(userId));
    }

    public IEnumerable<IFavoriteDto> GetFavoritesByUserId(string userId)
    {
        return _mapper.Map<IEnumerable<ContractFavoriteDto>>(_favoriteRepository.GetByUserId(userId));
    }
}
