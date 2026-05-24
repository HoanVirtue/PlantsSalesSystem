using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlantSales.API.Domain.Entities;

namespace PlantSales.API.Infrastructure.Data.Configurations;

public class CustomerConfiguration : BaseEntityConfiguration<Customer>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(c => c.FullName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(c => c.PhoneNumber)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(c => c.Email)
            .HasMaxLength(255);

        builder.Property(c => c.Note);

        builder.Property(c => c.CustomerType)
            .HasMaxLength(50);

        // Indices for filtering
        builder.HasIndex(c => c.PhoneNumber);
        builder.HasIndex(c => c.CustomerType);

        // Relationships
        builder.HasMany(c => c.Orders)
            .WithOne(o => o.Customer)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.ChatConversations)
            .WithOne(cc => cc.Customer)
            .HasForeignKey(cc => cc.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
