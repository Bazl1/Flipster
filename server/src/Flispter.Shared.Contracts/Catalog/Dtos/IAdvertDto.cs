namespace Flipster.Shared.Contracts.Catalog.Dtos;

public interface IAdvertDto
{
    string Id { get; set; }
    string Title { get; set; }
    string Description { get; set; }
    List<string> Images { get; set; }
    bool IsFree { get; set; }
    string Price { get; set; }
    string BusinessType { get; set; }
    string ProductType { get; set; }
    string Status { get; set; }
    string CreatedAt { get; set; }
    ICategoryDto Category { get; set; }
    IContactDto Contact { get; set; }
}
