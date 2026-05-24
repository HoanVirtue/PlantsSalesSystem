using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlantSales.API.Domain.Entities;

namespace PlantSales.API.Infrastructure.Data.Configurations;

public class PotShapeConfiguration : BaseEntityConfiguration<PotShape>
{
    protected override void ConfigureEntity(EntityTypeBuilder<PotShape> builder)
    {
        builder.Property(ps => ps.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(ps => ps.Description);

        // Unique index on Name
        builder.HasIndex(ps => ps.Name).IsUnique();
    }
}
