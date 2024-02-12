using Flipster.Modules.Identity.Domain.Common;

namespace Flipster.Modules.Identity.Domain.User.Services;

public interface IUserService
{
    IdentityResult ChangePassword(Entities.User user, string currentPassword, string newPassword);
    IdentityResult ChangeUser(Entities.User user);
    Entities.User? FindByEmail(string email);
    Entities.User? FindById(string id);
}