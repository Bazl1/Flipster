using Microsoft.AspNetCore.Http;

namespace Flipster.Modules.Adverts.Dto;

public class AdvertCreateRequest
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int CategoryId { get; set; }
    public List<IFormFile> Images { get; set; } = null!;
    public bool IsFree { get; set; }
    public decimal Price { get; set; }
    public string ProductType { get; set; } = null!;
    public string BusinessType { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}