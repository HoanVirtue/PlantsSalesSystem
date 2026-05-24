using System.Linq.Expressions;
using PlantSales.API.Domain.Entities;

namespace PlantSales.API.Application.Interfaces;

/// <summary>
/// Repository interface following ABP DDD pattern
/// Combines read-only operations with write operations
/// </summary>
public interface IRepository<T> : IReadOnlyRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Inserts a new entity
    /// </summary>
    Task<T> InsertAsync(T entity, bool autoSave = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Inserts multiple entities
    /// </summary>
    Task InsertManyAsync(IEnumerable<T> entities, bool autoSave = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an entity
    /// </summary>
    Task<T> UpdateAsync(T entity, bool autoSave = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates multiple entities
    /// </summary>
    Task UpdateManyAsync(IEnumerable<T> entities, bool autoSave = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an entity
    /// </summary>
    Task DeleteAsync(T entity, bool autoSave = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes entity by id
    /// </summary>
    Task DeleteAsync(long id, bool autoSave = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes multiple entities
    /// </summary>
    Task DeleteManyAsync(IEnumerable<T> entities, bool autoSave = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes entities matching predicate
    /// </summary>
    Task DeleteAsync(Expression<Func<T, bool>> predicate, bool autoSave = true, CancellationToken cancellationToken = default);
}
