using Flipster.Modules.Adverts.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Flipster.Modules.Adverts.Data.Seeds;

public class CategorySeed
{
    public void Seed(IServiceProvider serviceProvider)
    {
        using var _db = serviceProvider.GetRequiredService<AdvertsDbContext>();
        _db.Categories.AddRange(
            new Category(1, "Free", ""),
            new Category(2, "Cars", ""),
            new Category(3, "Works", ""),
            new Category(4, "Animals", ""),
            new Category(5, "Electronics", ""),
            new Category(6, "Clothes", ""),
            new Category(7, "Business", ""));
        _db.SaveChanges();
    }
}