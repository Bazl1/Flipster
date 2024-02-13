namespace Flipster.Modules.Identity.Infrastructure.Token;

public class TokenOptions
{
    public string SecretKey { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int AccessExpiry { get; set; }
    public int RefreshExpiry { get; set; }
}