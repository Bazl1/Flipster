using Flipster.Identity.Core.Dtos;

namespace Flipster.Identity.Core.Features.Login;

public record LoginResponse(
    UserDto User,
    string AccessToken,
    string RefreshToken
);