using Flipster.Modules.Identity.Domain.Common;

namespace Flipster.Modules.Identity.Domain.Token.Errors;

public class InvalidTokenError : ErrorBase
{
    public InvalidTokenError() : base("Invalid token.")
    {

    }
}