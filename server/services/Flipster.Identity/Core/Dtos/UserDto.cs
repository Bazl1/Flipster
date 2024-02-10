using Flipster.Identity.Core.Domain.Entities;
using Flipster.Identity.Core.Domain.Enums;

#pragma warning disable CS8601

namespace Flipster.Identity.Core.Dtos;

public class UserDto
{
    public string Id { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Role { get; set; } = null!;

    public static UserDto From(User user)
        => new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.UserName,
            Role = UserRoles.User.ToString(),
        };
}