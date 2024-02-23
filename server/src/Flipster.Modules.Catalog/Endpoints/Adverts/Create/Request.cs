using Microsoft.AspNetCore.Http;

namespace Flipster.Modules.Catalog.Endpoints.Adverts.Create;

internal record Request(
    string Title,
    string Description,
    string CategoryId,
    IFormFileCollection Images,
    bool IsFree,
    decimal Price,
    string ProductType,
    string BusinessType,
    string Location,
    string Email,
    string PhoneNumber);
