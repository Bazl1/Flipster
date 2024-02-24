using Flipster.Modules.Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Flipster.Modules.Users.Infrastructure.Persistence.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(u => u.Token).WithOne(t => t.User);
        builder.HasOne(u => u.Location).WithMany(l => l.Users);
        builder.HasMany(u => u.Favorites).WithOne(f => f.User);
    }
}
