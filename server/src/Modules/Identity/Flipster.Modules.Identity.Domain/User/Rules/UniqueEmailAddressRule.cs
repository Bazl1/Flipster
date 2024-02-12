using Flipster.Modules.Identity.Domain.Common;
using Flipster.Modules.Identity.Domain.User.Repositories;

namespace Flipster.Modules.Identity.Domain.User.Rules;

public class UniqueEmailAddressRule(
    IUserRepository _userRepository
) : IRule
{
    public bool Validate(Entities.User user)
    {
        if (_userRepository.FindByEmail(user.Email) is not null)
        {
            return false;
        }
        return true;
    }
}