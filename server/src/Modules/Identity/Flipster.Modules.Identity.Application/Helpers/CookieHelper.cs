using Flipster.Modules.Identity.Domain.Token.Entities;
using Microsoft.AspNetCore.Http;

namespace Flipster.Modules.Identity.Application.Helpers;

internal static class CookieHelper
{
    public const string COOKIE_AUTH = "Filpster.Auth";

    public static void AddToken(IHttpContextAccessor httpContextAccessor, Token token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = token.Expiry,
        };
        httpContextAccessor.HttpContext.Response.Cookies.Append(COOKIE_AUTH, token.Value);
    }

    public static void RemoveToken(IHttpContextAccessor httpContextAccessor)
    {
        httpContextAccessor.HttpContext.Response.Cookies.Delete(COOKIE_AUTH);
    }

    public static string GetToken(IHttpContextAccessor httpContextAccessor)
    {
        return httpContextAccessor.HttpContext.Request.Cookies[COOKIE_AUTH];
    }
}