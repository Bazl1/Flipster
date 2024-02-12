using Flipster.Modules.Identity.Domain.Common;

namespace Flipster.Modules.Identity.Domain.Token.Errors;

public class TokenExpiredError : ErrorBase
{
    public TokenExpiredError() : base("The token has expired.")
    {

    }
}