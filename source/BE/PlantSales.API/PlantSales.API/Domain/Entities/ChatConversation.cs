namespace PlantSales.API.Domain.Entities;

public class ChatConversation : BaseEntity
{
    public long? CustomerId { get; set; }
    public string? SessionId { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }

    public virtual Customer? Customer { get; set; }
    public virtual ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
}
