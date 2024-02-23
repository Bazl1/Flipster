using Microsoft.AspNetCore.Http;

namespace Flipster.Modules.Catalog.Endpoints.Adverts.Change;

internal record Request(
    string Title,
    string Description,
    string CategoryId,
    bool IsFree,
    decimal Price,
    string ProductType,
    string BusinessType,
    string Location,
    string Email,
    string PhoneNumber)
{
    public IEnumerable<string>? Urls { get; set; } = null;
    public IFormFileCollection? Images { get; set; } = null;
}
