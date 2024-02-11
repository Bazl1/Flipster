namespace Flipster.Identity.Features.ChangePassword;

public record ChangePasswordRequest(
    string OldPassword,
    string Password
);