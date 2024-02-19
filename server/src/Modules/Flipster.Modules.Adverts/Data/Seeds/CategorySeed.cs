using Flipster.Modules.Adverts.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Flipster.Modules.Adverts.Data.Seeds;

public class CategorySeed
{
    public void Seed(IServiceProvider serviceProvider)
    {
        using var _db = serviceProvider.GetRequiredService<AdvertsDbContext>();
        _db.Categories.AddRange(
            new Category(1, "Free", "http://localhost:5247/icons/free-icon.svg"),
            new Category(2, "Cars", "http://localhost:5247/icons/cars-icon.svg"),
            new Category(3, "Works", "http://localhost:5247/icons/works-icon.svg"),
            new Category(4, "Animals", "http://localhost:5247/icons/animals-icon.svg"),
            new Category(5, "Electronics", "http://localhost:5247/icons/electronics-icon.svg"),
            new Category(6, "Clothes", "http://localhost:5247/icons/clothes-icon.svg"),
            new Category(7, "Business", "http://localhost:5247/icons/business-icon.svg"));
        _db.SaveChanges();
    }
}