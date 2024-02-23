using Microsoft.AspNetCore.Http;

namespace Flipster.Modules.Users.Endpoints.Users.ChangeAvatar;

internal record Request(
    IFormFile Avatar);
