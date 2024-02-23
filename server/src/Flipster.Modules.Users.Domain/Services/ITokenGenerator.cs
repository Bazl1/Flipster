using Flipster.Modules.Users.Domain.Entities;

namespace Flipster.Modules.Users.Domain.Services;

public interface ITokenGenerator
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken(int size = 64);
}
