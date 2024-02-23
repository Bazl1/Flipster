using Flipster.Modules.Users.Domain.Entities;
using Flipster.Modules.Users.Domain.Services;
using Flipster.Modules.Users.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Flipster.Modules.Users.Infrastructure.Services;

internal class TokenGenerator : ITokenGenerator
{
    private readonly TokenOptions _tokenOptions;

    public TokenGenerator(
        IOptions<TokenOptions> options)
    {
        _tokenOptions = options.Value;
    }

    public string GenerateAccessToken(User user)
    {
        var securityKeyForAccessToken = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecretKey));
        var claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        var accessSecurityToken = new JwtSecurityToken(
            issuer: _tokenOptions.Issuer,
            audience: _tokenOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_tokenOptions.AccessExpiry - 5),
            signingCredentials: new SigningCredentials(securityKeyForAccessToken, SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler()
            .WriteToken(accessSecurityToken);
    }

    public string GenerateRefreshToken(int size = 64)
    {
        var randomNumber = new byte[size];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
