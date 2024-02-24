using Flipster.Shared.Contracts.Catalog.Dtos;

namespace Flipster.Modules.Catalog.Dtos;

public class AdvertDto : IAdvertDto
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required List<string> Images { get; set; }
    public required bool IsFree { get; set; }
    public required string Price { get; set; }
    public required string BusinessType { get; set; }
    public required string ProductType { get; set; }
    public required string Status { get; set; }
    public required string CreatedAt { get; set; }
    public required ICategoryDto Category { get; set; }
    public required IContactDto Contact { get; set; }
}
