using Flipster.Modules.Adverts.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Flipster.Modules.Adverts.Data.Seeds;

public class CategorySeed
{
    public void Seed(IServiceProvider serviceProvider)
    {
        using var _db = serviceProvider.GetRequiredService<AdvertsDbContext>();
        _db.Categories.AddRange(
            new Category(1, "Free", "free-icon.svg"),
            new Category(2, "Cars", "cars-icon.svg"),
            new Category(3, "Works", "works-icon.svg"),
            new Category(4, "Animals", "animals-icon.svg"),
            new Category(5, "Electronics", "electronics-icon.svg"),
            new Category(6, "Clothes", "clothes-icon.svg"),
            new Category(7, "Business", "business-icon.svg"));
        _db.SaveChanges();
    }
}