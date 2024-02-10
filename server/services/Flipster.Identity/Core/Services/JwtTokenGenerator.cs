using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Flipster.Identity.Core.Data;
using Flipster.Identity.Core.Domain.Entities;
using Flipster.Identity.Core.Services.Interfaces;
using Flipster.Identity.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

#pragma warning disable CS8604

namespace Flipster.Identity.Core.Services;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtOptions _jwtOptions;
    private readonly ApplicationDbContext _db;

    public JwtTokenGenerator(
        IOptions<JwtOptions> options,
        ApplicationDbContext db)
    {
        _jwtOptions = options.Value;
        _db = db;
    }

    public JwtResult GenerateToken(User user, Claim[] claims)
    {
        var securityKeyForAccessToken = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.AccessSecretKey));
        var accessSecurityToken = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwtOptions.AccessExpirationMinutes),
            signingCredentials: new SigningCredentials(securityKeyForAccessToken, SecurityAlgorithms.HmacSha256));

        var result = new JwtResult
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(accessSecurityToken),
            RefreshToken = GenerateToken()
        };

        if (_db.RefreshTokens.SingleOrDefault(refreshToken => refreshToken.UserId == user.Id) is not RefreshToken refreshToken)
        {
            _db.Add(RefreshToken.Create(user.Id, result.RefreshToken, _jwtOptions.RefreshExpirationMinutes));
        }
        else
        {
            refreshToken.Value = result.RefreshToken;
            refreshToken.Expiry = DateTime.Now.AddMinutes(_jwtOptions.RefreshExpirationMinutes);
        }
        _db.SaveChanges();

        return result;
    }

    private string GenerateToken(int size = 32)
    {
        var randomNumber = new byte[size];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}