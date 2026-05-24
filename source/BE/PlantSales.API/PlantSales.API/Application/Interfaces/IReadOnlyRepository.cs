using System.Linq.Expressions;

namespace PlantSales.API.Application.Interfaces;

/// <summary>
/// Read-only repository interface following ABP DDD pattern
/// </summary>
public interface IReadOnlyRepository<T> where T : class
{
    /// <summary>
    /// Gets queryable source (use with caution, prefer async methods)
    /// </summary>
    IQueryable<T> GetQueryable();

    /// <summary>
    /// Gets a queryable that includes specified related entities
    /// </summary>
    Task<IQueryable<T>> GetQueryableAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets entity by id, throws EntityNotFoundException if not found
    /// </summary>
    Task<T> GetByIdAsync(long id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets entity by id, returns null if not found
    /// </summary>
    Task<T?> FindByIdAsync(long id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds first entity matching predicate, throws if not found
    /// </summary>
    Task<T> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds first entity matching predicate, returns null if not found
    /// </summary>
    Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets list of entities matching predicate
    /// </summary>
    Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets list of all entities
    /// </summary>
    Task<List<T>> GetListAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets paginated list of entities
    /// </summary>
    Task<PagedResult<T>> GetPagedListAsync(
        int pageNumber,
        int pageSize,
        Expression<Func<T, bool>>? predicate = null,
        Expression<Func<T, object>>? orderBy = null,
        bool isDescending = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets total count of entities, optionally filtered
    /// </summary>
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if any entity matches predicate
    /// </summary>
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Includes related entities for eager loading
    /// </summary>
    IReadOnlyRepository<T> WithDetails(params Expression<Func<T, object>>[] navigationProperties);

    /// <summary>
    /// Includes related entities for eager loading (async)
    /// </summary>
    Task<IReadOnlyRepository<T>> WithDetailsAsync(
        Func<IReadOnlyRepository<T>, Task<IReadOnlyRepository<T>>> detailsFactory,
        CancellationToken cancellationToken = default);
}
