namespace Flipster.Modules.Identity.Domain.Common;

public class Empty { }
public class IdentityResult : IdentityResult<Empty>
{
    public static IdentityResult<Empty> Secceeded()
        => new IdentityResult<Empty> { IsSecceeded = true };
}

public class IdentityResult<TResult>
{
    public TResult? Result { get; set; } = default(TResult);
    public IError? Error { get; set; } = null;
    public bool IsSecceeded { get; set; }

    public static IdentityResult<TResult> Secceeded(TResult result)
        => new IdentityResult<TResult> { IsSecceeded = true, Result = result };

    public static IdentityResult<TResult> Failed(IError error)
        => new IdentityResult<TResult> { IsSecceeded = false, Error = error };
}