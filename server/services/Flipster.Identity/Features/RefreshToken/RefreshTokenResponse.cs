using Flipster.Identity.Core.Dtos;

namespace Flipster.Identity.Features.RefreshToken;

public record RefreshTokenResponse(
    UserDto User,
    string AccessToken,
    string RefreshToken
);