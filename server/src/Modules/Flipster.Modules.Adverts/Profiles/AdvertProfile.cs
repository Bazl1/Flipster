using AutoMapper;
using Flipster.Modules.Adverts.Dto;
using Flipster.Modules.Adverts.Entities;

namespace Flipster.Modules.Adverts.Profiles;

public class AdvertProfile : Profile
{
    public AdvertProfile()
    {
        CreateMap<Advert, AdvertDto>()
            .ForMember(dest => dest.Contact, opt => opt.MapFrom(src => new ContactDto
            {
                Id = src.SellerId,
                Email = src.Email,
                PhoneNumber = src.PhoneNumber,
                Location = src.Location
            }))
            .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => src.CreateAt.ToString()));
        CreateMap<AdvertUpdateRequest, Advert>();
    }
}