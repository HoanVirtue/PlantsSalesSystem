using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlantSales.API.Domain.Entities;

namespace PlantSales.API.Infrastructure.Data.Configurations;

public class BonsaiImageConfiguration : BaseEntityConfiguration<BonsaiImage>
{
    protected override void ConfigureEntity(EntityTypeBuilder<BonsaiImage> builder)
    {
        builder.Property(bi => bi.ImageUrl)
            .IsRequired();

        builder.Property(bi => bi.IsThumbnail)
            .HasDefaultValue(false);

        builder.Property(bi => bi.SortOrder)
            .HasDefaultValue(0);

        // Foreign key relationship
        builder.HasOne(bi => bi.BonsaiTree)
            .WithMany(b => b.Images)
            .HasForeignKey(bi => bi.BonsaiTreeId)
            .OnDelete(DeleteBehavior.Cascade);

        // Index for efficient filtering by bonsai tree
        builder.HasIndex(bi => bi.BonsaiTreeId);
    }
}
