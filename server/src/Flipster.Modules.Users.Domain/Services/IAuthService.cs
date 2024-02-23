using Flipster.Modules.Users.Domain.Entities;

namespace Flipster.Modules.Users.Domain.Services;

public interface IAuthService
{
    void Register(User user, string password);
    User Login(string email, string password);
    void Logout(User user);
    void Refresh(User user, string token);
}