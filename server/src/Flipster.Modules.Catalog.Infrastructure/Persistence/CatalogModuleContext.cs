using Flipster.Modules.Catalog.Domain.Entities;
using Flipster.Modules.Catalog.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Flipster.Modules.Catalog.Infrastructure.Persistence;

public class CatalogModuleContext : DbContext
{
    public DbSet<Advert> Adverts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<View> Views { get; set; }

    public CatalogModuleContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ViewConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
