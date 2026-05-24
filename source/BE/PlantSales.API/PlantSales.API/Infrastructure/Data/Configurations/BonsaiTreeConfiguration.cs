using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlantSales.API.Domain.Entities;
using PlantSales.API.Domain.Enums;

namespace PlantSales.API.Infrastructure.Data.Configurations;

public class BonsaiTreeConfiguration : BaseEntityConfiguration<BonsaiTree>
{
    protected override void ConfigureEntity(EntityTypeBuilder<BonsaiTree> builder)
    {
        // Properties with constraints
        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(b => b.Code)
            .HasMaxLength(50);

        builder.Property(b => b.Slug)
            .HasMaxLength(255);

        builder.Property(b => b.Description);

        builder.Property(b => b.ShortDescription)
            .HasMaxLength(1000);

        builder.Property(b => b.ThumbnailUrl);

        builder.Property(b => b.DisplayPrice)
            .HasMaxLength(100);

        // Decimal properties
        builder.Property(b => b.ImportPrice).HasColumnType("decimal(18,2)");
        builder.Property(b => b.ActualPrice).HasColumnType("decimal(18,2)");
        builder.Property(b => b.TrunkCircumference).HasColumnType("decimal(10,2)");
        builder.Property(b => b.CanopyWidth).HasColumnType("decimal(10,2)");
        builder.Property(b => b.CanopyHeight).HasColumnType("decimal(10,2)");

        // Enum conversion
        builder.Property(b => b.Status)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(b => b.CareInstruction);

        // Indices
        builder.HasIndex(b => b.Code).IsUnique();
        builder.HasIndex(b => b.Slug);
        builder.HasIndex(b => b.Status);

        // Foreign key relationships
        builder.HasOne(b => b.TreeStyle)
            .WithMany(ts => ts.BonsaiTrees)
            .HasForeignKey(b => b.TreeStyleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.PotShape)
            .WithMany(ps => ps.BonsaiTrees)
            .HasForeignKey(b => b.PotShapeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.PotSize)
            .WithMany(psize => psize.BonsaiTrees)
            .HasForeignKey(b => b.PotSizeId)
            .OnDelete(DeleteBehavior.Restrict);

        // Collection relationships
        builder.HasMany(b => b.Images)
            .WithOne(bi => bi.BonsaiTree)
            .HasForeignKey(bi => bi.BonsaiTreeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(b => b.Orders)
            .WithOne(o => o.BonsaiTree)
            .HasForeignKey(o => o.BonsaiTreeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
