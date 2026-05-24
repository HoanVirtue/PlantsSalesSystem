using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlantSales.API.Domain.Entities;

namespace PlantSales.API.Infrastructure.Data.Configurations;

public class OrderStatusHistoryConfiguration : BaseEntityConfiguration<OrderStatusHistory>
{
    protected override void ConfigureEntity(EntityTypeBuilder<OrderStatusHistory> builder)
    {
        builder.Property(osh => osh.OldStatus)
            .HasMaxLength(50);

        builder.Property(osh => osh.NewStatus)
            .HasMaxLength(50);

        // Foreign key relationships
        builder.HasOne(osh => osh.Order)
            .WithMany(o => o.StatusHistories)
            .HasForeignKey(osh => osh.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(osh => osh.ChangedByUser)
            .WithMany()
            .HasForeignKey(osh => osh.ChangedBy)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        // Index for efficient querying
        builder.HasIndex(osh => osh.OrderId);
    }
}
