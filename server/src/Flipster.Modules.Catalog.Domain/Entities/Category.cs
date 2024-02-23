using Flipster.Shared.Domain.Entities;

namespace Flipster.Modules.Catalog.Domain.Entities;

public class Category : EntityBase, IAggregate
{
    public required string Title { get; set; }
    public required string Icon { get; set; }

    public List<Advert> Adverts { get; set; } = new();
}
