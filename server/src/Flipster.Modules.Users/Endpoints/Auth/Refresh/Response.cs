using Flipster.Modules.Users.Dtos;

namespace Flipster.Modules.Users.Endpoints.Auth.Refresh;

internal record Response(
    string AccessToken,
    UserDto User);
