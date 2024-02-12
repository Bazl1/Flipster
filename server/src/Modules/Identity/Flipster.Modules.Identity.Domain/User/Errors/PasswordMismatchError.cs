using Flipster.Modules.Identity.Domain.Common;

namespace Flipster.Modules.Identity.Domain.User.Errors;

public class PasswordMismatchError : ErrorBase
{
    public PasswordMismatchError() : base("The passwords don't match.")
    {

    }
}