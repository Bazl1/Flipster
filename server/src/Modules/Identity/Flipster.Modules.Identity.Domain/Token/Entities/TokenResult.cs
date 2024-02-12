namespace Flipster.Modules.Identity.Domain.Token.Entities;

public class TokenResult
{
    public string Access { get; set; }
    public string Refresh { get; set; }

    public TokenResult(string access, string refresh)
    {
        Access = access;
        Refresh = refresh;
    }
}