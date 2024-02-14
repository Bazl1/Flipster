using Flipster.Modules.Adverts.Entities;

namespace Flipster.Modules.Adverts.Services;

public interface ICategoryService
{
    List<Category> GetAll();
    Category? GetById(int id);
    void Create(Category category);
    void Update(Category category);
    void Remove(int id);
}