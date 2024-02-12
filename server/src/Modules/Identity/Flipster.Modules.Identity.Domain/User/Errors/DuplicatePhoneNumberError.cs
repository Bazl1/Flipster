using Flipster.Modules.Identity.Domain.Common;

namespace Flipster.Modules.Identity.Domain.User.Errors;

public class DuplicatePhoneNumberError : ErrorBase
{
    public DuplicatePhoneNumberError(string phoneNumber) : base($"User with this phone number '{phoneNumber}' already exists.")
    {

    }
}