namespace Flipster.Shared.Domain.Errors;

public class FlipsterError : Exception
{
    public FlipsterError(string message) : base(message)
    {

    }
}