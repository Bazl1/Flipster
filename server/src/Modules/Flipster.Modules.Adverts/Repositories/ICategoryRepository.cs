using Flipster.Modules.Adverts.Entities;

namespace Flipster.Modules.Adverts.Repositories;

public interface ICategoryRepository
{
    List<Category> GetAll();
}