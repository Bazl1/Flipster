using Flipster.Modules.Adverts.Entities;

namespace Flipster.Modules.Adverts.Repositories;

public interface IAdvertRepository
{
    void Create(Advert advert);
}