using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlantSales.API.Domain.Entities;

namespace PlantSales.API.Infrastructure.Data.Configurations;

/// <summary>
/// Base configuration for all entities
/// Handles common properties: Id, CreatedAt, UpdatedAt, IsDeleted, ConcurrencyStamp, ExtraProperties
/// </summary>
public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        // Primary key
        builder.HasKey(e => e.Id);

        // Audit fields
        builder.Property(e => e.CreatedAt)
            .IsRequired(false);

        builder.Property(e => e.UpdatedAt)
            .IsRequired(false);

        // Soft delete
        builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        // Concurrency stamp (row version)
        builder.Property(e => e.ConcurrencyStamp)
            .IsRowVersion()
            .IsConcurrencyToken();

        // Extra properties as JSON with value converter
        builder.Property(e => e.ExtraProperties)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<Dictionary<string, object>>(v, (JsonSerializerOptions)null) ?? new Dictionary<string, object>())
            .HasColumnType("json");

        // Configure child properties specific to derived entity type
        ConfigureEntity(builder);
    }

    /// <summary>
    /// Override this method in derived classes to configure entity-specific properties
    /// </summary>
    protected abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
}
