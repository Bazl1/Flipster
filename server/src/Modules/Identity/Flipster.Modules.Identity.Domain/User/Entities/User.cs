using Flipster.Modules.Identity.Domain.User.Enums;
using Flipster.Modules.Identity.Domain.User.ValueObjects;

namespace Flipster.Modules.Identity.Domain.User.Entities;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public UserRole Role { get; set; }
    public string Avatar { get; set; } = null!;
    public Location Location { get; set; } = new Location(string.Empty);
}