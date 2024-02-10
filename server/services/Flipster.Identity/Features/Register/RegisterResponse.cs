using Flipster.Identity.Core.Dtos;

namespace Flipster.Identity.Features.Register;

public record RegisterResponse(
    UserDto User,
    string AccessToken,
    string RefreshToken
);