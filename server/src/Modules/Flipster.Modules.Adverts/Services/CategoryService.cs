using Flipster.Modules.Adverts.Entities;
using Flipster.Modules.Adverts.Repositories;

namespace Flipster.Modules.Adverts.Services;

public class CategoryService(
    ICategoryRepository _categoryRepository) : ICategoryService
{
    public List<Category> GetAll()
    {
        return _categoryRepository.GetAll();
    }

    public Category? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Create(Category category)
    {
        throw new NotImplementedException();
    }

    public void Update(Category category)
    {
        throw new NotImplementedException();
    }

    public void Remove(int id)
    {
        throw new NotImplementedException();
    }
}