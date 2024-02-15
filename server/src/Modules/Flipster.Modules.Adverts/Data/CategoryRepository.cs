using Flipster.Modules.Adverts.Entities;
using Flipster.Modules.Adverts.Repositories;

namespace Flipster.Modules.Adverts.Data;

public class CategoryRepository(
    AdvertsDbContext _db) : ICategoryRepository
{
    public List<Category> GetAll()
    {
        return _db.Categories.ToList();
    }

    public Category? GetById(int id)
    {
        return _db.Categories.SingleOrDefault(category => category.Id == id);
    }
}