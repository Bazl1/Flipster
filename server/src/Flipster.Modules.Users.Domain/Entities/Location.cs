using Flipster.Shared.Domain.Entities;

namespace Flipster.Modules.Users.Domain.Entities;

public class Location : EntityBase, IAggregate
{
    public required string Value { get; set; }

    public List<User> Users { get; set; } = new();
}
