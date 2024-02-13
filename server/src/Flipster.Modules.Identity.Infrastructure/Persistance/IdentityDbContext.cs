using Microsoft.EntityFrameworkCore;

namespace Flipster.Modules.Identity.Domain.Infrastructure.Persistance;

public class IdentityDbContext : DbContext
{
    public DbSet<Domain.User.Entities.User> Users { get; set; }
    public DbSet<Domain.User.Entities.Token> Tokens { get; set; }

    public IdentityDbContext(DbContextOptions options) : base(options)
    {
    }
}