using AutoMapper;
using Flipster.Modules.Catalog.Domain.Entities;
using Flipster.Modules.Catalog.Dtos;

namespace Flipster.Modules.Catalog.Profiles;

internal class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>();
    }
}
