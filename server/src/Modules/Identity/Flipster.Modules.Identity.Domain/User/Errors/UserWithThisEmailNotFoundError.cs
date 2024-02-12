using Flipster.Modules.Identity.Domain.Common;

namespace Flipster.Modules.Identity.Domain.User.Errors;

public class UserWithThisEmailNotFoundError : ErrorBase
{
    public UserWithThisEmailNotFoundError(string email) : base($"No user with this email '{email}' was found.")
    {
    }
}