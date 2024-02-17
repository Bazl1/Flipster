using Flipster.Modules.Adverts.Entities;
using Microsoft.EntityFrameworkCore;

namespace Flipster.Modules.Adverts.Data;

public class AdvertsDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Advert> Adverts { get; set; }

    public AdvertsDbContext(DbContextOptions<AdvertsDbContext> options) : base(options)
    {
    }
}