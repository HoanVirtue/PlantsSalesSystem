using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlantSales.API.Domain.Entities;

namespace PlantSales.API.Infrastructure.Data.Configurations;

public class TreeStyleConfiguration : BaseEntityConfiguration<TreeStyle>
{
    protected override void ConfigureEntity(EntityTypeBuilder<TreeStyle> builder)
    {
        builder.Property(ts => ts.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(ts => ts.Slug)
            .HasMaxLength(255);

        builder.Property(ts => ts.Description);

        // Unique index on Name
        builder.HasIndex(ts => ts.Name).IsUnique();

        // Regular index on Slug for filtering
        builder.HasIndex(ts => ts.Slug);
    }
}
