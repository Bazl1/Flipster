using Flipster.Modules.Identity.Domain.Token.Entities;
using Microsoft.AspNetCore.Http;

namespace Flipster.Modules.Identity.Utils;

public static class CookieUtils
{
    public const string COOKIE_AUTH = "Filpster.Auth";

    public static void AddToken(HttpContext context, Token token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = token.Expiry,
        };
        context.Response.Cookies.Append(COOKIE_AUTH, token.Value);
    }

    public static void RemoveToken(HttpContext context)
    {
        context.Response.Cookies.Delete(COOKIE_AUTH);
    }

    public static string GetToken(HttpContext context)
    {
        return context.Request.Cookies[COOKIE_AUTH];
    }
}