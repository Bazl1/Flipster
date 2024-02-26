using AutoMapper;
using Flipster.Modules.Catalog.Domain.Repositories;
using Flipster.Modules.Catalog.Dtos;
using Flipster.Shared.Contracts.Catalog.Dtos;
using Flispter.Shared.Contracts.Catalog;
using Flispter.Shared.Contracts.Users;

namespace Flipster.Modules.Catalog.Contracts;

public class CatalogModule(
    IAdvertRepository _advertRepository,
    IViewRepository _viewRepository,
    IUsersModule _usersModule,
    IMapper _mapper) : ICatalogModule
{
    public IAdvertDto? GetAdvertById(string id)
    {
        return _mapper.Map<AdvertDto>(_advertRepository.GetById(id));
    }

    public IEnumerable<IAdvertDto> GetAll()
    {
        return _mapper.Map<IEnumerable<AdvertDto>>(
            _advertRepository.GetAll()
            .Select(a =>
            {
                var seller = _usersModule.GetUserById(a.SellerId);
                return new AdvertDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Images = a.Images,
                    IsFree = a.IsFree,
                    Price = a.Price != null ? a.Price.ToString() : string.Empty,
                    BusinessType = a.BusinessType.ToString(),
                    ProductType = a.ProductType.ToString(),
                    Status = a.Status.ToString(),
                    CreatedAt = a.CreatedAt.ToString(),
                    Category = _mapper.Map<CategoryDto>(a.Category),
                    Contact = new ContactDto { Id = a.Id, Name = seller.Name, Avatar = seller.Avatar, Email = a.Email, Location = a.Location, PhoneNumber = a.PhoneNumber },
                    Views = _viewRepository.GetCountByAdvertId(a.Id)
                };
            }));
    }
}