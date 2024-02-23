using Flipster.Shared.Domain.Entities;

namespace Flipster.Modules.Users.Domain.Entities;

public class Query : EntityBase, IAggregate
{
    public required string QueryString { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public required string UserId { get; set; }
    public User? User { get; set; }
}