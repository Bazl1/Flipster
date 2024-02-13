namespace Flipster.Modules.Identity.Dtos.ChangePassword;

public record ChangePasswordRequest(
    string CurrentPassword,
    string NewPassword);