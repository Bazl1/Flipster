using Microsoft.AspNetCore.Http;

namespace Flipster.Modules.Adverts.Dto;

public class AdvertUpdateRequest
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int CategoryId { get; set; }
    public List<string> ImageUrls { get; set; } = null!;
    public IFormFileCollection Images { get; set; } = null!;
    public bool IsFree { get; set; }
    public decimal Price { get; set; } = 0.00m;
    public string ProductType { get; set; } = null!;
    public string BusinessType { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}