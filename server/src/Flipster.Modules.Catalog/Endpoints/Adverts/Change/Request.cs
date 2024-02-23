using Microsoft.AspNetCore.Http;

namespace Flipster.Modules.Catalog.Endpoints.Adverts.Change;

internal record Request(
    string Title,
    string Description,
    string CategoryId,
    bool IsFree,
    string ProductType,
    string BusinessType,
    string Location,
    string Email,
    string PhoneNumber)
{
    public decimal Price { get; set; } = 0.00m;
    public IEnumerable<string>? ImageUrls { get; set; } = null;
    public IFormFileCollection? Images { get; set; } = null;
}
