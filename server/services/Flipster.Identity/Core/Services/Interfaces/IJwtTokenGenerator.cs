using System.Security.Claims;
using Flipster.Identity.Core.Domain.Entities;

namespace Flipster.Identity.Core.Services.Interfaces;

public interface IJwtTokenGenerator
{
    JwtResult GenerateToken(User user, Claim[] claims);
}