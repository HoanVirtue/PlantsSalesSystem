namespace PlantSales.API.Domain.Entities;

public class OrderStatusHistory : BaseEntity
{
    public long OrderId { get; set; }
    public string? OldStatus { get; set; }
    public string? NewStatus { get; set; }
    public long? ChangedBy { get; set; }

    public virtual Order Order { get; set; } = null!;
    public virtual User? ChangedByUser { get; set; }
}
