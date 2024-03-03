using Flipster.Shared.Contracts.Catalog.Dtos;

namespace Flispter.Shared.Contracts.Catalog;

public interface ICatalogModule
{
    IAdvertDto? GetAdvertById(string id);
    IEnumerable<IAdvertDto> GetAll();
    void UpdateViews(string visitorId, string userId);
}