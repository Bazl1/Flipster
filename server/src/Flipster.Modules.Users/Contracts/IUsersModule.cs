using Flipster.Modules.Users.Dtos;

namespace Flipster.Modules.Users.Contracts;

public interface IUsersModule
{
    UserDto? GetUserById(string userId);
}
