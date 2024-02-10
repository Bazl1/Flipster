namespace Flipster.Identity.Core.Services;

public class JwtResult
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}