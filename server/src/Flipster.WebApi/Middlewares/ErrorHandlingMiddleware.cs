using Flipster.Shared.Domain.Errors;
using System.Text.Json;

namespace Flipster.WebApi.Middlewares;

public class ErrorHandlingMiddleware(
    RequestDelegate _next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleException(context, exception);
        }
    }

    private static Task HandleException(HttpContext context, Exception exception)
    {
        string result;
        switch (exception)
        {
            case FlipsterError applicationError:
                result = JsonSerializer.Serialize(
                    new { Error = applicationError.Message },
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                break;

            default:
                result = JsonSerializer.Serialize(
                    new { Error = "An error occurred while executing the request The server is temporarily not responding to requests." },
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                break;    
        }

        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(result);
    }
}