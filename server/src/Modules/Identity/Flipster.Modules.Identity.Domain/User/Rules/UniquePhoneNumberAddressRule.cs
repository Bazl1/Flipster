using Flipster.Modules.Identity.Domain.Common;
using Flipster.Modules.Identity.Domain.User.Repositories;

namespace Flipster.Modules.Identity.Domain.User.Rules;

public class UniquePhoneNumberAddressRule(
    IUserRepository _userRepository
) : IRule
{
    public bool Validate(Entities.User user)
    {
        if (user.PhoneNumber == null || string.IsNullOrWhiteSpace(user.PhoneNumber))
        {
            return true;
        }
        if (_userRepository.FindByPhoneNumber(user.PhoneNumber) is not null)
        {
            return false;
        }
        return true;
    }
}