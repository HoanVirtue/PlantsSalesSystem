using Microsoft.EntityFrameworkCore.Storage;
using PlantSales.API.Application.Interfaces;
using PlantSales.API.Domain.Entities;
using PlantSales.API.Infrastructure.Data;

namespace PlantSales.API.Infrastructure.Repositories;

/// <summary>
/// Unit of Work pattern implementation for managing transactions and repositories
/// Lazy-loads repositories on first access
/// </summary>
public class UnitOfWork : IUnitOfWork, IAsyncDisposable
{
    private readonly AppDbContext _dbContext;
    private IDbContextTransaction? _transaction;

    // Repository field cache (lazy initialization)
    private IRepository<User>? _users;
    private IRepository<TreeStyle>? _treeStyles;
    private IRepository<PotShape>? _potShapes;
    private IRepository<PotSize>? _potSizes;
    private IRepository<BonsaiTree>? _bonsaiTrees;
    private IRepository<BonsaiImage>? _bonsaiImages;
    private IRepository<Customer>? _customers;
    private IRepository<Order>? _orders;
    private IRepository<OrderPayment>? _orderPayments;
    private IRepository<OrderStatusHistory>? _orderStatusHistories;
    private IRepository<TelegramNotificationLog>? _telegramNotificationLogs;
    private IRepository<ChatConversation>? _chatConversations;
    private IRepository<ChatMessage>? _chatMessages;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Repository properties with lazy initialization
    public IRepository<User> Users => _users ??= new GenericRepository<User>(_dbContext);
    public IRepository<TreeStyle> TreeStyles => _treeStyles ??= new GenericRepository<TreeStyle>(_dbContext);
    public IRepository<PotShape> PotShapes => _potShapes ??= new GenericRepository<PotShape>(_dbContext);
    public IRepository<PotSize> PotSizes => _potSizes ??= new GenericRepository<PotSize>(_dbContext);
    public IRepository<BonsaiTree> BonsaiTrees => _bonsaiTrees ??= new GenericRepository<BonsaiTree>(_dbContext);
    public IRepository<BonsaiImage> BonsaiImages => _bonsaiImages ??= new GenericRepository<BonsaiImage>(_dbContext);
    public IRepository<Customer> Customers => _customers ??= new GenericRepository<Customer>(_dbContext);
    public IRepository<Order> Orders => _orders ??= new GenericRepository<Order>(_dbContext);
    public IRepository<OrderPayment> OrderPayments => _orderPayments ??= new GenericRepository<OrderPayment>(_dbContext);
    public IRepository<OrderStatusHistory> OrderStatusHistories => _orderStatusHistories ??= new GenericRepository<OrderStatusHistory>(_dbContext);
    public IRepository<TelegramNotificationLog> TelegramNotificationLogs => _telegramNotificationLogs ??= new GenericRepository<TelegramNotificationLog>(_dbContext);
    public IRepository<ChatConversation> ChatConversations => _chatConversations ??= new GenericRepository<ChatConversation>(_dbContext);
    public IRepository<ChatMessage> ChatMessages => _chatMessages ??= new GenericRepository<ChatMessage>(_dbContext);

    /// <summary>
    /// Saves all pending changes to database
    /// </summary>
    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Begins a database transaction
    /// </summary>
    public async Task<bool> BeginTransactionAsync()
    {
        _transaction = await _dbContext.Database.BeginTransactionAsync();
        return true;
    }

    /// <summary>
    /// Commits the current transaction
    /// </summary>
    public async Task<bool> CommitTransactionAsync()
    {
        try
        {
            await SaveChangesAsync();
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }
            return true;
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    /// <summary>
    /// Rolls back the current transaction
    /// </summary>
    public async Task<bool> RollbackTransactionAsync()
    {
        try
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
            return true;
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    /// <summary>
    /// Disposes the UnitOfWork and cleans up resources
    /// </summary>
    public void Dispose()
    {
        _transaction?.Dispose();
        _dbContext.Dispose();
    }

    /// <summary>
    /// Async disposal
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }
        await _dbContext.DisposeAsync();
    }
}
