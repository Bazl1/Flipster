using AutoMapper;
using Flipster.Modules.Catalog.Domain.Repositories;
using Flipster.Modules.Catalog.Dtos;
using Flipster.Shared.Contracts.Catalog.Dtos;
using Flispter.Shared.Contracts.Catalog;

namespace Flipster.Modules.Catalog.Contracts;

public class CatalogModule(
    IAdvertRepository _advertRepository,
    IMapper _mapper) : ICatalogModule
{
    public IAdvertDto? GetAdvertById(string id)
    {
        return _mapper.Map<AdvertDto>(_advertRepository.GetById(id));
    }
}