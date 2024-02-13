using System.Security.Claims;

namespace Flipster.Modules.Identity.Domain.Token.Services;

public interface ITokenGenerator
{
    string GenerateAccessToken(Claim[] claims);
    Token.Entities.Token GenerateRefreshToken(int size = 32);
}