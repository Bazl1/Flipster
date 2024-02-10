namespace Flipster.Identity.Core.Features.Register;

public record RegisterRequest(
    string Email,
    string Name,
    string Password
);