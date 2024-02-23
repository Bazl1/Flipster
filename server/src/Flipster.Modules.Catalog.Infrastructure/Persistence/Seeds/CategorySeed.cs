using Flipster.Modules.Catalog.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Flipster.Modules.Catalog.Infrastructure.Persistence.Seeds;

public class CategorySeed
{
    public void Seed(IServiceProvider serviceProvider)
    {
        using var _db = serviceProvider.GetRequiredService<CatalogModuleContext>();
        _db.Categories.AddRange(
            new Category { Title = "Free", Icon = "http://localhost:5145/icons/free-icon.svg" },
            new Category { Title = "Cars", Icon = "http://localhost:5145/icons/cars-icon.svg" },
            new Category { Title = "Works", Icon = "http://localhost:5145/icons/works-icon.svg" },
            new Category { Title = "Animals", Icon = "http://localhost:5145/icons/animals-icon.svg" },
            new Category { Title = "Electronics", Icon = "http://localhost:5145/icons/electronics-icon.svg" },
            new Category { Title = "Clothes", Icon = "http://localhost:5145/icons/clothes-icon.svg" },
            new Category { Title = "Business", Icon = "http://localhost:5145/icons/business-icon.svg" });
        _db.SaveChanges();
    }
}
