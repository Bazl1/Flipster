using AutoMapper;
using Flipster.Modules.Catalog.Domain.Repositories;
using Flipster.Modules.Catalog.Dtos;
using Flipster.Shared.Contracts.Catalog.Dtos;
using Flipster.Shared.Domain.Errors;
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
        var advert = _advertRepository.GetById(id);
        if (advert == null)
            throw new FlipsterError("Advert with given id is not found.");
        var seller = _usersModule.GetUserById(advert.SellerId);
        return new AdvertDto
        {
            Id = advert.Id,
            Title = advert.Title,
            Description = advert.Description,
            Images = advert.Images,
            IsFree = advert.IsFree,
            Price = advert.Price != null ? advert.Price.ToString() : string.Empty,
            BusinessType = advert.BusinessType.ToString(),
            ProductType = advert.ProductType.ToString(),
            Status = advert.Status.ToString(),
            CreatedAt = advert.CreatedAt.ToString(),
            Category = _mapper.Map<CategoryDto>(advert.Category),
            Contact = new ContactDto { Id = advert.SellerId, Name = seller.Name, Avatar = seller.Avatar, Email = advert.Email, Location = advert.Location, PhoneNumber = advert.PhoneNumber },
            Views = _viewRepository.GetCountByAdvertId(advert.Id)
        };
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
                    Contact = new ContactDto { Id = a.SellerId, Name = seller.Name, Avatar = seller.Avatar, Email = a.Email, Location = a.Location, PhoneNumber = a.PhoneNumber },
                    Views = _viewRepository.GetCountByAdvertId(a.Id)
                };
            }));
    }
}