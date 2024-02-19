using AutoMapper;
using Flipster.Modules.Identity.Domain.User.Entities.Contracts.Abstractions;
using Flipster.Modules.Identity.Domain.User.Repositories;
using Flipster.Modules.Identity.Dtos;

namespace Flipster.Modules.Identity.Domain.User.Entities.Contracts;

public class UserModule(
    IUserRepository _userRepository,
    IMapper _mapper
) : IUserModule
{
    public UserDto? GetById(string id)
    {
        if (_userRepository.FindById(id) is not User user)
            return null;
        return _mapper.Map<UserDto>(user);
    }
}