using Flipster.Modules.Catalog.Domain.Entities;
using Flipster.Modules.Catalog.Domain.Repositories;

namespace Flipster.Modules.Catalog.Infrastructure.Persistence.Repositories;

internal class CategoryRepository(
    CatalogModuleContext _db) : ICategoryRepository
{
    public void Add(Category entity)
    {
        _db.Add(entity);
        _db.SaveChanges();
    }

    public IEnumerable<Category> GetAll()
    {
        return _db.Categories;
    }

    public Category? GetById(string id)
    {
        return _db.Categories
            .SingleOrDefault(category => category.Id == id);
    }

    public void Remove(Category entity)
    {
        _db.Remove(entity);
        _db.SaveChanges();
    }

    public void Update(Category entity)
    {
        _db.SaveChanges();
    }
}
