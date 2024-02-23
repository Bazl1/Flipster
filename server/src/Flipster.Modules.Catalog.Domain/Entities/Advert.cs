using Flipster.Modules.Catalog.Domain.Enums;
using Flipster.Shared.Domain.Entities;

namespace Flipster.Modules.Catalog.Domain.Entities;

public class Advert : EntityBase, IAggregateRoot
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public List<string> Images { get; set; } = new();
    public required BusinessType BusinessType { get; set; }
    public required ProductType ProductType { get; set; }
    public required bool IsFree { get; set; }
    public decimal? Price { get; set; }

    public required string CategoryId { get; set; }
    public Category? Category { get; set; }

    public required string SellerId { get; set; }
    public required string Location { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }

    public Status Status { get; set; } = Status.Active;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
