using Flipster.Modules.Users.Dtos;

namespace Flipster.Modules.Users.Endpoints.Auth.Register;

internal record Response(
    string AccessToken,
    string AntiforgeryToken,
    UserDto User);
