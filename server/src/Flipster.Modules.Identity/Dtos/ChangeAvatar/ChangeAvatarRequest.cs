using Microsoft.AspNetCore.Http;

namespace Flipster.Modules.Identity.Dtos.ChangePassword;

public record ChangeAvatarRequest(
    IFormFile Avatar);