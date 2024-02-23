namespace Flipster.Modules.Users.Endpoints.Auth.Login;

internal record Request(
    string Email,
    string Password);