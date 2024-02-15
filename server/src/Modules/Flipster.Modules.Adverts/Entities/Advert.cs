using Flipster.Modules.Adverts.Enums;
using Flipster.Modules.Adverts.ValueObjects;

namespace Flipster.Modules.Adverts.Entities;

#pragma warning disable

public class Advert
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int CategoryId { get; set; }
    public List<string>? Images { get; set; }
    public AdvertProductType ProductType { get; set; }
    public AdvertBusinessType BusinessType { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;
    public AdvertPrice Price { get; set; }
    public AdvertContact Contact { get; set; }
}