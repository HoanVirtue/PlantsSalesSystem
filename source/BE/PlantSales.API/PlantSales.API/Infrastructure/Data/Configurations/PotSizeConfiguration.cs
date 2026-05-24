using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlantSales.API.Domain.Entities;

namespace PlantSales.API.Infrastructure.Data.Configurations;

public class PotSizeConfiguration : BaseEntityConfiguration<PotSize>
{
    protected override void ConfigureEntity(EntityTypeBuilder<PotSize> builder)
    {
        builder.Property(ps => ps.SizeCm)
            .IsRequired();

        builder.Property(ps => ps.Description);

        // Index on SizeCm for filtering
        builder.HasIndex(ps => ps.SizeCm);
    }
}
