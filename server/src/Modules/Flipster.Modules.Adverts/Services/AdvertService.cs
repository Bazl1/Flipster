using Flipster.Modules.Adverts.Entities;
using Flipster.Modules.Adverts.Repositories;

namespace Flipster.Modules.Adverts.Services;

public class AdvertService(
    IAdvertRepository advertRepository) : IAdvertService
{
    public void Create(Advert advert)
    {
        advertRepository.Create(advert);
    }
}