namespace Flipster.Modules.Users.Infrastructure.Auth;

public static class FlipsterAuthenticationSchemes
{
    public static class CookieScheme
    {
        public const string SchemeName = "Flipster.Cookies.Visitor";
        public const string CookieName = "Flipster.Authentication";
    }
}
