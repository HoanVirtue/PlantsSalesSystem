using PlantSales.API.Domain.Entities;

namespace PlantSales.API.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> Users { get; }
    IRepository<TreeStyle> TreeStyles { get; }
    IRepository<PotShape> PotShapes { get; }
    IRepository<PotSize> PotSizes { get; }
    IRepository<BonsaiTree> BonsaiTrees { get; }
    IRepository<BonsaiImage> BonsaiImages { get; }
    IRepository<Customer> Customers { get; }
    IRepository<Order> Orders { get; }
    IRepository<OrderPayment> OrderPayments { get; }
    IRepository<OrderStatusHistory> OrderStatusHistories { get; }
    IRepository<TelegramNotificationLog> TelegramNotificationLogs { get; }
    IRepository<ChatConversation> ChatConversations { get; }
    IRepository<ChatMessage> ChatMessages { get; }

    Task<int> SaveChangesAsync();
    Task<bool> BeginTransactionAsync();
    Task<bool> CommitTransactionAsync();
    Task<bool> RollbackTransactionAsync();
}
