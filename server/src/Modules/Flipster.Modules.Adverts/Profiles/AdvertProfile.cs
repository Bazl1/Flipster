using AutoMapper;
using Flipster.Modules.Adverts.Dto;
using Flipster.Modules.Adverts.Entities;
using Flipster.Modules.Adverts.ValueObjects;

namespace Flipster.Modules.Adverts.Profiles;

public class AdvertProfile : Profile
{
    public AdvertProfile()
    {
        CreateMap<AdvertContact, ContactDto>();
        CreateMap<Advert, AdvertDto>()
            .ForMember(dest => dest.IsFree, opt => opt.MapFrom(src => src.Price.IsFree))
            .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => src.CreateAt.ToString()))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.IsFree ? 0.00m : src.Price.Value));
        CreateMap<AdvertCreateRequest, Advert>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => new AdvertPrice(src.IsFree, src.Price)));
    }
}