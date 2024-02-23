namespace Flipster.WebApi.Middlewares;

public class LoggingMiddleware(
    RequestDelegate _next,
    ILogger<ErrorHandlingMiddleware> _logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError($"Request: {context.Request.Path}\nMessage: {exception.Message}");
            throw exception;
        }
    }
}