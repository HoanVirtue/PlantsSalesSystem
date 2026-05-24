namespace PlantSales.API.Domain.Entities;

public class TelegramNotificationLog : BaseEntity
{
    public long? OrderId { get; set; }
    public string? MessageContent { get; set; }
    public bool IsSuccess { get; set; }
    public string? Response { get; set; }

    public virtual Order? Order { get; set; }
}
