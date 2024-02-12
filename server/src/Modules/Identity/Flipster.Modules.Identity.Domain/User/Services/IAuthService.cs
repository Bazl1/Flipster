using Flipster.Modules.Identity.Domain.Common;

namespace Flipster.Modules.Identity.Domain.User.Services;

public interface IAuthService
{
    IdentityResult SingUp(Entities.User user, string password);
    IdentityResult SingInWithEmail(string email, string password);
    IdentityResult SingOut(Entities.User user);
}