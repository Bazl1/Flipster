using Flipster.Modules.Identity.Domain.Common;

namespace Flipster.Modules.Identity.Domain.User.Errors;

public class DuplicateEmailError : ErrorBase
{
    public DuplicateEmailError(string email) : base($"User with this email '{email}' already exists.")
    {

    }
}