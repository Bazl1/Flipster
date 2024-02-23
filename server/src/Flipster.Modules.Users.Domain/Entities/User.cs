using Flipster.Modules.Users.Domain.Enums;
using Flipster.Shared.Domain.Entities;

namespace Flipster.Modules.Users.Domain.Entities;

public class User : EntityBase, IAggregateRoot
{
    public required string Name { get; set; }
    public string? Avatar { get; set; }

    public string PasswordHash { get; set; } = null!;
    
    public UserRole Role { get; set; }
    
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }

    public Token? Token { get; set; }
    
    public string? LocationId { get; set; }
    public Location? Location { get; set; }

    // public List<Favorite> Favorites { get; set; } = new();
    
    // public List<View> Views { get; set; } = new();

    // public List<Query> Queries { get; set; } = new();
}