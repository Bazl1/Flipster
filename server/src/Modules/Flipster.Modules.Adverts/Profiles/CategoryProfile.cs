using AutoMapper;
using Flipster.Modules.Adverts.Dto;
using Flipster.Modules.Adverts.Entities;

namespace Flipster.Modules.Adverts.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>();
    }
}