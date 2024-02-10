namespace Flipster.Identity.Features.Register;

public record RegisterRequest(
    string Email,
    string Name,
    string Password
);