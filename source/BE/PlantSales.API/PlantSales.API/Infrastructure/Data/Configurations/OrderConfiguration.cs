using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlantSales.API.Domain.Entities;
using PlantSales.API.Domain.Enums;

namespace PlantSales.API.Infrastructure.Data.Configurations;

public class OrderConfiguration : BaseEntityConfiguration<Order>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Order> builder)
    {
        builder.Property(o => o.OrderCode)
            .HasMaxLength(50);

        builder.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");
        builder.Property(o => o.PaidAmount).HasColumnType("decimal(18,2)");
        builder.Property(o => o.RemainingAmount).HasColumnType("decimal(18,2)");

        // Enum conversion
        builder.Property(o => o.Status)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(o => o.CompletedAt);

        // Indices
        builder.HasIndex(o => o.OrderCode).IsUnique();
        builder.HasIndex(o => o.Status);

        // Foreign key relationships
        builder.HasOne(o => o.BonsaiTree)
            .WithMany(b => b.Orders)
            .HasForeignKey(o => o.BonsaiTreeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.CreatedByUser)
            .WithMany()
            .HasForeignKey(o => o.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);

        // Collection relationships
        builder.HasMany(o => o.Payments)
            .WithOne(op => op.Order)
            .HasForeignKey(op => op.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(o => o.StatusHistories)
            .WithOne(osh => osh.Order)
            .HasForeignKey(osh => osh.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(o => o.NotificationLogs)
            .WithOne(tnl => tnl.Order)
            .HasForeignKey(tnl => tnl.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
