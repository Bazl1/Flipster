using Flipster.Shared.Contracts.Catalog.Dtos;

namespace Flispter.Shared.Contracts.Catalog;

public interface ICatalogModule
{
    IAdvertDto? GetAdvertById(string id);
}