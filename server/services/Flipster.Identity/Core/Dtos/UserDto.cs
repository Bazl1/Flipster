using Flipster.Identity.Core.Domain.Entities;

#pragma warning disable CS8601

namespace Flipster.Identity.Core.Dtos;

public class UserDto
{
    public string Id { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string Avatar { get; set; } = null!;

    public static UserDto From(User user, string role)
        => new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber ?? string.Empty,
            Name = user.Name,
            Role = role,
            Avatar = user.Avatar ?? string.Empty,
        };
}