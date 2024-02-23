using Microsoft.AspNetCore.Http;

namespace Flipster.Modules.Catalog.Endpoints.Adverts.Create;

internal record Request(
    string Title,
    string Description,
    string CategoryId,
    IFormFileCollection Images,
    bool IsFree,
    string ProductType,
    string BusinessType,
    string Location,
    string Email,
    string PhoneNumber)
{
    public decimal Price { get; set; } = 0.00m;
}