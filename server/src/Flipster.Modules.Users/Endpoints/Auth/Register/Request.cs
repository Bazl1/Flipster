namespace Flipster.Modules.Users.Endpoints.Auth.Register;

internal record Request(
    string Name,
    string Email,
    string Password);