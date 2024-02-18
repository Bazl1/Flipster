using Flipster.Modules.Adverts.Enums;

namespace Flipster.Modules.Adverts.Entities;

#pragma warning disable

public class Advert
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public List<string> Images { get; set; } = new();
    public AdvertProductType ProductType { get; set; }
    public AdvertBusinessType BusinessType { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;
    public bool IsFree { get; set; }
    public decimal Price { get; set; }
    public string SellerId { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}