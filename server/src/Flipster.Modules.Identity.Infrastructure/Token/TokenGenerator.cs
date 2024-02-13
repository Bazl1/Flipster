using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Flipster.Modules.Identity.Domain.Token.Services;
using Flipster.Modules.Identity.Infrastructure.Persistance;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Flipster.Modules.Identity.Infrastructure.Token;

public class TokenGenerator : ITokenGenerator
{
    private readonly TokenOptions _tokenOptions;
    private readonly IdentityDbContext _db;

    public TokenGenerator(
        IOptions<TokenOptions> options,
        IdentityDbContext db)
    {
        _tokenOptions = options.Value;
        _db = db;
    }
    
    public string GenerateAccessToken(Claim[] claims)
    {
        var securityKeyForAccessToken = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecretKey));
        var accessSecurityToken = new JwtSecurityToken(
            issuer: _tokenOptions.Issuer,
            audience: _tokenOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_tokenOptions.AccessExpiry - 5),
            signingCredentials: new SigningCredentials(securityKeyForAccessToken, SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler()
            .WriteToken(accessSecurityToken);
    }

    public Domain.Token.Entities.Token GenerateRefreshToken(int size = 32)
    {
        var randomNumber = new byte[size];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return new Domain.Token.Entities.Token
        {
            Value = Convert.ToBase64String(randomNumber),
            Expiry = DateTime.UtcNow.AddMinutes(_tokenOptions.RefreshExpiry),
        };
    }
}