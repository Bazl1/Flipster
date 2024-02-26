using Flipster.Shared.Domain.Entities;

namespace Flipster.Modules.Catalog.Domain.Entities;

public class View : IAggregate
{
    public required string AdvertId { get; set; }
    public required string UserId { get; set; }
}
