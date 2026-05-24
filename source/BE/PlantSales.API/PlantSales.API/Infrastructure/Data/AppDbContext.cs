using Microsoft.EntityFrameworkCore;
using PlantSales.API.Domain.Entities;
using PlantSales.API.Infrastructure.Data.Configurations;

namespace PlantSales.API.Infrastructure.Data;

/// <summary>
/// Database context for Plant Sales API
/// Implements soft delete pattern with global query filters
/// Entity-specific configurations are in separate Configuration files (IEntityTypeConfiguration pattern)
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<TreeStyle> TreeStyles { get; set; }
    public DbSet<PotShape> PotShapes { get; set; }
    public DbSet<PotSize> PotSizes { get; set; }
    public DbSet<BonsaiTree> BonsaiTrees { get; set; }
    public DbSet<BonsaiImage> BonsaiImages { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderPayment> OrderPayments { get; set; }
    public DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }
    public DbSet<TelegramNotificationLog> TelegramNotificationLogs { get; set; }
    public DbSet<ChatConversation> ChatConversations { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply global query filters for soft delete
        modelBuilder.Entity<User>().HasQueryFilter(m => !m.IsDeleted);
        modelBuilder.Entity<TreeStyle>().HasQueryFilter(m => !m.IsDeleted);
        modelBuilder.Entity<PotShape>().HasQueryFilter(m => !m.IsDeleted);
        modelBuilder.Entity<PotSize>().HasQueryFilter(m => !m.IsDeleted);
        modelBuilder.Entity<BonsaiTree>().HasQueryFilter(m => !m.IsDeleted);
        modelBuilder.Entity<BonsaiImage>().HasQueryFilter(m => !m.IsDeleted);
        modelBuilder.Entity<Customer>().HasQueryFilter(m => !m.IsDeleted);
        modelBuilder.Entity<Order>().HasQueryFilter(m => !m.IsDeleted);
        modelBuilder.Entity<OrderPayment>().HasQueryFilter(m => !m.IsDeleted);
        modelBuilder.Entity<OrderStatusHistory>().HasQueryFilter(m => !m.IsDeleted);
        modelBuilder.Entity<TelegramNotificationLog>().HasQueryFilter(m => !m.IsDeleted);
        modelBuilder.Entity<ChatConversation>().HasQueryFilter(m => !m.IsDeleted);
        modelBuilder.Entity<ChatMessage>().HasQueryFilter(m => !m.IsDeleted);

        // Apply all entity configurations
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new TreeStyleConfiguration());
        modelBuilder.ApplyConfiguration(new PotShapeConfiguration());
        modelBuilder.ApplyConfiguration(new PotSizeConfiguration());
        modelBuilder.ApplyConfiguration(new BonsaiTreeConfiguration());
        modelBuilder.ApplyConfiguration(new BonsaiImageConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderPaymentConfiguration());
        modelBuilder.ApplyConfiguration(new OrderStatusHistoryConfiguration());
        modelBuilder.ApplyConfiguration(new TelegramNotificationLogConfiguration());
        modelBuilder.ApplyConfiguration(new ChatConversationConfiguration());
        modelBuilder.ApplyConfiguration(new ChatMessageConfiguration());
    }
}
