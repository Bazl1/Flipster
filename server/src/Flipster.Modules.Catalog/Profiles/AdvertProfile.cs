using AutoMapper;
using Flipster.Modules.Catalog.Domain.Entities;
using Flipster.Modules.Catalog.Domain.Enums;
using Flipster.Modules.Catalog.Dtos;

namespace Flipster.Modules.Catalog.Profiles;

internal class AdvertProfile : Profile
{
    public AdvertProfile()
    {
        CreateMap<Endpoints.Adverts.Create.Request, Advert>()
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => Enum.Parse<ProductType>(src.ProductType)))
            .ForMember(dest => dest.BusinessType, opt => opt.MapFrom(src => Enum.Parse<BusinessType>(src.BusinessType)))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => new List<string>()));

        CreateMap<Advert, AdvertDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType.ToString()))
            .ForMember(dest => dest.BusinessType, opt => opt.MapFrom(src => src.BusinessType.ToString()));
    }
}
