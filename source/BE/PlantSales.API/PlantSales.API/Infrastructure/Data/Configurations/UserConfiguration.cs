using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlantSales.API.Domain.Entities;

namespace PlantSales.API.Infrastructure.Data.Configurations;

public class UserConfiguration : BaseEntityConfiguration<User>
{
    protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.Property(u => u.FullName)
            .HasMaxLength(255);

        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(30);

        builder.Property(u => u.Email)
            .HasMaxLength(255);

        builder.Property(u => u.AvatarUrl)
            .HasMaxLength(512);

        // Indices for frequently queried fields
        builder.HasIndex(u => u.Username);
        builder.HasIndex(u => u.Email);
    }
}
