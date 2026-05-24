namespace PlantSales.API.Domain.Entities;

public class Customer : BaseEntity
{
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string? Email { get; set; }
    public string? Note { get; set; }
    public string CustomerType { get; set; } = "NEW"; // NEW, POTENTIAL, VIP, BOUGHT, SPAM

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ICollection<ChatConversation> ChatConversations { get; set; } = new List<ChatConversation>();
}
