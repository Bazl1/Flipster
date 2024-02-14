using Flipster.Modules.Adverts.Entities;
using Flipster.Modules.Adverts.Repositories;

namespace Flipster.Modules.Adverts.Data;

public class CategoryRepository(
    AdvertsDbContext _advertsDbContext) : ICategoryRepository
{
    public List<Category> GetAll()
    {
        return _advertsDbContext.Categories.ToList();
    }
}