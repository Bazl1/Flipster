namespace Flipster.Modules.Users.Endpoints.Users.ChangePassword;

internal record Request(
    string CurrentPassword,
    string NewPassword);
