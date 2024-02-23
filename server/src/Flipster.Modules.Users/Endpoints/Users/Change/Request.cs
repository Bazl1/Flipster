namespace Flipster.Modules.Users.Endpoints.Users.Change;

internal record Request(
    string? Name = null,
    string? Location = null,
    string? PhoneNumber = null);
