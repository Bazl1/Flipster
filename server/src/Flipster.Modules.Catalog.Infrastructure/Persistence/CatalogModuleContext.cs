using Flipster.Modules.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Flipster.Modules.Catalog.Infrastructure.Persistence;

public class CatalogModuleContext : DbContext
{
    public DbSet<Advert> Adverts { get; set; }
    public DbSet<Category> Categories { get; set; }

    public CatalogModuleContext(DbContextOptions options) : base(options)
    {
    }
}
