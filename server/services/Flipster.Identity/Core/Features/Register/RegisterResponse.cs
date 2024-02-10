using Flipster.Identity.Core.Dtos;

namespace Flipster.Identity.Core.Features.Register;

public record RegisterResponse(
    UserDto User,
    string AccessToken,
    string RefreshToken
);