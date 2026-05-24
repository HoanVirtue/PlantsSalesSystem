namespace PlantSales.API.Domain.Entities;

using PlantSales.API.Domain.Enums;

public class ChatMessage : BaseEntity
{
    public long ConversationId { get; set; }
    public ChatSenderRoleEnum SenderRole { get; set; }
    public string Message { get; set; } = null!;

    public virtual ChatConversation Conversation { get; set; } = null!;
}
