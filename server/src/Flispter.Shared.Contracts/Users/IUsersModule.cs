using Flispter.Shared.Contracts.Users.Dtos;

namespace Flispter.Shared.Contracts.Users;

public interface IUsersModule
{
    IUserDto? GetUserById(string userId);
}