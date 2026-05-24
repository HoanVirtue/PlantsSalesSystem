using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlantSales.API.Domain.Entities;

namespace PlantSales.API.Infrastructure.Data.Configurations;

public class TelegramNotificationLogConfiguration : BaseEntityConfiguration<TelegramNotificationLog>
{
    protected override void ConfigureEntity(EntityTypeBuilder<TelegramNotificationLog> builder)
    {
        builder.Property(tnl => tnl.MessageContent);

        builder.Property(tnl => tnl.IsSuccess)
            .HasDefaultValue(false);

        builder.Property(tnl => tnl.Response);

        // Foreign key relationship (optional - Order)
        builder.HasOne(tnl => tnl.Order)
            .WithMany(o => o.NotificationLogs)
            .HasForeignKey(tnl => tnl.OrderId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        // Index for efficient querying
        builder.HasIndex(tnl => tnl.OrderId);
    }
}
