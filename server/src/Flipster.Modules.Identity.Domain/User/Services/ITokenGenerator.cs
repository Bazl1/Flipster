using System.Security.Claims;

namespace Flipster.Modules.Identity.Domain.User.Services;

public interface ITokenGenerator
{
    string GenerateAccessToken(Claim[] claims);
    Entities.Token GenerateRefreshToken(int size = 32);
}