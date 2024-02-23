using Flipster.Modules.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Flipster.Modules.Catalog.Infrastructure.Persistence.Configurations;

internal class AdvertConfiguration : IEntityTypeConfiguration<Advert>
{
    public void Configure(EntityTypeBuilder<Advert> builder)
    {
        builder
            .HasOne(advert => advert.Category)
            .WithMany(category => category.Adverts);
    }
}
