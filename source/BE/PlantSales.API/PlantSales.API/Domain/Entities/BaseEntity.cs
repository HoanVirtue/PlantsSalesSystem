using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlantSales.API.Domain.Entities;

/// <summary>
/// Base entity class for all domain entities following DDD principles
/// Provides soft delete, audit tracking, and optimistic locking support
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Primary key
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Soft delete flag - when true, entity is logically deleted
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Creation timestamp (UTC)
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Last update timestamp (UTC)
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Concurrency stamp for optimistic locking (similar to SQL Server RowVersion)
    /// Auto-generated on create, updated on each modification
    /// </summary>
    [Timestamp]
    public byte[]? ConcurrencyStamp { get; set; }

    /// <summary>
    /// Extra properties for flexible data storage (JSON in database)
    /// Useful for storing additional data without schema migration
    /// </summary>
    [JsonIgnore]
    public Dictionary<string, object> ExtraProperties { get; set; } = [];

    /// <summary>
    /// Sets an extra property value
    /// </summary>
    public void SetExtraProperty(string key, object? value)
    {
        if (value == null)
        {
            ExtraProperties.Remove(key);
        }
        else
        {
            ExtraProperties[key] = value;
        }
    }

    /// <summary>
    /// Gets an extra property value
    /// </summary>
    public object? GetExtraProperty(string key)
    {
        return ExtraProperties.TryGetValue(key, out var value) ? value : null;
    }

    /// <summary>
    /// Gets an extra property value with type conversion
    /// </summary>
    public T? GetExtraProperty<T>(string key) where T : class
    {
        var value = GetExtraProperty(key);
        return value as T;
    }
}
