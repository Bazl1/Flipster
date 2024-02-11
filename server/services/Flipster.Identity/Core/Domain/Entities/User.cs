using Flipster.Identity.Core.Domain.Enums;
using Flipster.Identity.Features.Register;
using Microsoft.AspNetCore.Identity;

namespace Flipster.Identity.Core.Domain.Entities;

public class User : IdentityUser
{
    public string Avatar { get; set; } = null!;
    public string Name { get; set; } = null!;
    public Location Location { get; set; } = null!;
    public UserState State { get; set; } = UserState.Active;

    public static User From(RegisterRequest request)
        => new User
        {
            Name = request.Name,
            Email = request.Email,
            UserName = request.Email,
            Avatar = string.Empty,
            Location = new Location(string.Empty)
        };
}