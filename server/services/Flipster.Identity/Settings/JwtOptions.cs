namespace Flipster.Identity.Settings;

public class JwtOptions
{
    public string AccessSecretKey { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public int AccessExpirationMinutes { get; set; }
    public int RefreshExpirationMinutes { get; set; }
}