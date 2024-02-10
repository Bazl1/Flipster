namespace Flipster.Identity.Core.Features.Login;

public record LoginRequest(
    string Email,
    string Password
);