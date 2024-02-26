using Flipster.Modules.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Flipster.Modules.Catalog.Infrastructure.Persistence.Configurations;

internal class ViewConfiguration : IEntityTypeConfiguration<View>
{
    public void Configure(EntityTypeBuilder<View> builder)
    {
        builder.HasKey(v => new { v.AdvertId, v.UserId });
    }
}
