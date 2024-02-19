using Flipster.Modules.Identity.Dtos;

namespace Flipster.Modules.Identity.Domain.User.Entities.Contracts.Abstractions;

public interface IUserModule
{
    UserDto? GetById(string id);
}