using System.Security.Claims;
using Flipster.Modules.Identity.Domain.Token.Entities;
using Flipster.Modules.Identity.Domain.Token.Services;

namespace Flipster.Modules.Identity.Application.Services;

internal class TokenGenerator : ITokenGenerator
{
    public string GenerateAccessToken(Claim[] claims)
    {
        throw new NotImplementedException();
    }

    Token ITokenGenerator.GenerateRefreshToken(int size)
    {
        throw new NotImplementedException();
    }
}