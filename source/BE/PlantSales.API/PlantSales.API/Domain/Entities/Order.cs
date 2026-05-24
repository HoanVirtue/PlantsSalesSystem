namespace PlantSales.API.Domain.Entities;

using PlantSales.API.Domain.Enums;

public class Order : BaseEntity
{
    public string? OrderCode { get; set; }
    public long BonsaiTreeId { get; set; }
    public long CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; } = 0;
    public decimal RemainingAmount { get; set; } = 0;
    public OrderStatusEnum Status { get; set; } = OrderStatusEnum.Pending;
    public DateTime? CompletedAt { get; set; }
    public long CreatedBy { get; set; }

    public virtual BonsaiTree BonsaiTree { get; set; } = null!;
    public virtual Customer Customer { get; set; } = null!;
    public virtual User CreatedByUser { get; set; } = null!;
    public virtual ICollection<OrderPayment> Payments { get; set; } = new List<OrderPayment>();
    public virtual ICollection<OrderStatusHistory> StatusHistories { get; set; } = new List<OrderStatusHistory>();
    public virtual ICollection<TelegramNotificationLog> NotificationLogs { get; set; } = new List<TelegramNotificationLog>();
}
