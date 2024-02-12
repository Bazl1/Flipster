namespace Flipster.Modules.Identity.Domain.Common;

public class ErrorBase : IError
{
    public string Message { get; set; }

    public ErrorBase(string message)
    {
        Message = message;
    }
}