using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlantSales.API.Domain.Entities;

namespace PlantSales.API.Infrastructure.Data.Configurations;

public class OrderPaymentConfiguration : BaseEntityConfiguration<OrderPayment>
{
    protected override void ConfigureEntity(EntityTypeBuilder<OrderPayment> builder)
    {
        builder.Property(op => op.Amount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(op => op.PaymentDate)
            .IsRequired();

        // Foreign key relationships
        builder.HasOne(op => op.Order)
            .WithMany(o => o.Payments)
            .HasForeignKey(op => op.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(op => op.CreatedByUser)
            .WithMany()
            .HasForeignKey(op => op.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);

        // Index for efficient querying
        builder.HasIndex(op => op.OrderId);
    }
}
