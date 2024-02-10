namespace Flipster.Identity.Features.Login;

public record LoginRequest(
    string Email,
    string Password
);