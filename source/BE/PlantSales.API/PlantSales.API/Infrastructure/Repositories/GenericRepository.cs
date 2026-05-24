using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PlantSales.API.Application.Interfaces;
using PlantSales.API.Domain.Entities;
using PlantSales.API.Infrastructure.Data;

namespace PlantSales.API.Infrastructure.Repositories;

/// <summary>
/// Generic repository implementation following ABP DDD pattern
/// Supports soft delete, eager loading, pagination, and optimistic locking
/// </summary>
public class GenericRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<T> _dbSet;
    private IQueryable<T> _queryable;

    public GenericRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
        _queryable = _dbSet.AsQueryable();
    }

    /// <summary>
    /// Gets queryable source (ignores query filters, not recommended for production)
    /// </summary>
    public IQueryable<T> GetQueryable()
    {
        return _queryable;
    }

    /// <summary>
    /// Gets queryable with query filters applied (includes soft delete filter)
    /// </summary>
    public Task<IQueryable<T>> GetQueryableAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_queryable);
    }

    /// <summary>
    /// Gets entity by id, throws EntityNotFoundException if not found
    /// </summary>
    public async Task<T> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbSet.FindAsync(new object[] { id }, cancellationToken);
        if (entity == null)
        {
            throw new InvalidOperationException($"Entity of type {typeof(T).Name} with id {id} was not found.");
        }
        return entity;
    }

    /// <summary>
    /// Gets entity by id, returns null if not found
    /// </summary>
    public async Task<T?> FindByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
    }

    /// <summary>
    /// Gets first entity matching predicate, throws if not found
    /// </summary>
    public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var entity = await _queryable.FirstOrDefaultAsync(predicate, cancellationToken);
        if (entity == null)
        {
            throw new InvalidOperationException($"No entity of type {typeof(T).Name} matches the given predicate.");
        }
        return entity;
    }

    /// <summary>
    /// Gets first entity matching predicate, returns null if not found
    /// </summary>
    public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _queryable.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    /// <summary>
    /// Gets list of entities matching predicate
    /// </summary>
    public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _queryable.Where(predicate).ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Gets all entities
    /// </summary>
    public async Task<List<T>> GetListAsync(CancellationToken cancellationToken = default)
    {
        return await _queryable.ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Gets paginated list of entities
    /// </summary>
    public async Task<PagedResult<T>> GetPagedListAsync(
        int pageNumber,
        int pageSize,
        Expression<Func<T, bool>>? predicate = null,
        Expression<Func<T, object>>? orderBy = null,
        bool isDescending = false,
        CancellationToken cancellationToken = default)
    {
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 10;

        var query = _queryable;

        // Apply filter
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        // Count total before pagination
        var totalCount = await query.CountAsync(cancellationToken);

        // Apply ordering
        if (orderBy != null)
        {
            query = isDescending
                ? query.OrderByDescending(orderBy)
                : query.OrderBy(orderBy);
        }

        // Apply pagination
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<T>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    /// <summary>
    /// Gets total count of entities, optionally filtered
    /// </summary>
    public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        return predicate == null
            ? await _queryable.CountAsync(cancellationToken)
            : await _queryable.CountAsync(predicate, cancellationToken);
    }

    /// <summary>
    /// Checks if any entity matches predicate
    /// </summary>
    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _queryable.AnyAsync(predicate, cancellationToken);
    }

    /// <summary>
    /// Includes related entities for eager loading (sync version)
    /// </summary>
    public IReadOnlyRepository<T> WithDetails(params Expression<Func<T, object>>[] navigationProperties)
    {
        foreach (var property in navigationProperties)
        {
            _queryable = _queryable.Include(property);
        }
        return this;
    }

    /// <summary>
    /// Includes related entities for eager loading (async version)
    /// </summary>
    public async Task<IReadOnlyRepository<T>> WithDetailsAsync(
        Func<IReadOnlyRepository<T>, Task<IReadOnlyRepository<T>>> detailsFactory,
        CancellationToken cancellationToken = default)
    {
        return await detailsFactory(this);
    }

    /// <summary>
    /// Inserts a new entity
    /// </summary>
    public async Task<T> InsertAsync(T entity, bool autoSave = true, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        if (autoSave)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        return entity;
    }

    /// <summary>
    /// Inserts multiple entities
    /// </summary>
    public async Task InsertManyAsync(IEnumerable<T> entities, bool autoSave = true, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
        if (autoSave)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    /// <summary>
    /// Updates an entity
    /// </summary>
    public async Task<T> UpdateAsync(T entity, bool autoSave = true, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        if (autoSave)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        return entity;
    }

    /// <summary>
    /// Updates multiple entities
    /// </summary>
    public async Task UpdateManyAsync(IEnumerable<T> entities, bool autoSave = true, CancellationToken cancellationToken = default)
    {
        _dbSet.UpdateRange(entities);
        if (autoSave)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    /// <summary>
    /// Deletes an entity (soft delete via IsDeleted flag)
    /// </summary>
    public async Task DeleteAsync(T entity, bool autoSave = true, CancellationToken cancellationToken = default)
    {
        entity.IsDeleted = true;
        _dbSet.Update(entity);
        if (autoSave)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    /// <summary>
    /// Deletes entity by id (soft delete)
    /// </summary>
    public async Task DeleteAsync(long id, bool autoSave = true, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        await DeleteAsync(entity, autoSave, cancellationToken);
    }

    /// <summary>
    /// Deletes multiple entities (soft delete)
    /// </summary>
    public async Task DeleteManyAsync(IEnumerable<T> entities, bool autoSave = true, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            entity.IsDeleted = true;
        }
        _dbSet.UpdateRange(entities);
        if (autoSave)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    /// <summary>
    /// Deletes entities matching predicate (soft delete)
    /// </summary>
    public async Task DeleteAsync(Expression<Func<T, bool>> predicate, bool autoSave = true, CancellationToken cancellationToken = default)
    {
        var entities = await _queryable.Where(predicate).ToListAsync(cancellationToken);
        await DeleteManyAsync(entities, autoSave, cancellationToken);
    }
}
