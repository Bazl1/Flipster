using Flipster.Modules.Users.Domain.Entities;
using Flipster.Modules.Users.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Flipster.Modules.Users.Infrastructure.Persistence;

public class UsersModuleDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Token> Tokens { get; set; }
    public DbSet<Location> Locations { get; set; }

    public UsersModuleDbContext(DbContextOptions<UsersModuleDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
