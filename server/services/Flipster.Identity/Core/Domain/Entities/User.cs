using Flipster.Identity.Core.Domain.Enums;
using Flipster.Identity.Core.Features.Register;
using Microsoft.AspNetCore.Identity;

namespace Flipster.Identity.Core.Domain.Entities;

public class User : IdentityUser
{
    public string Avatar { get; set; } = null!;
    public UserState State { get; set; } = UserState.Active;

    public static User From(RegisterRequest request)
        => new User
        {
            UserName = request.Name,
            Email = request.Email,
            Avatar = string.Empty,
        };
}