namespace Flipster.Modules.Adverts.Dto;

public class AdvertDto
{
    public string Id { get; set; } = null!;
    
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<string>? Images { get; set; } = null!;
    public CategoryDto Category { get; set; } = null!;
    public string ProductType { get; set; } = null!;
    public string BusinessType { get; set; } = null!;
    public string CreateAt { get; set; } = null!;
    public bool IsFree { get; set; }
    public string Price { get; set; } = string.Empty;
    public ContactDto Contact { get; set; } = null!;
}