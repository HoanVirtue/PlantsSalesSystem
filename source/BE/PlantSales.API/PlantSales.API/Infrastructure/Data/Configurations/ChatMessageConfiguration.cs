using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlantSales.API.Domain.Entities;
using PlantSales.API.Domain.Enums;

namespace PlantSales.API.Infrastructure.Data.Configurations;

public class ChatMessageConfiguration : BaseEntityConfiguration<ChatMessage>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ChatMessage> builder)
    {
        builder.Property(cm => cm.Message)
            .IsRequired();

        // Enum conversion
        builder.Property(cm => cm.SenderRole)
            .HasConversion<string>()
            .HasMaxLength(50);

        // Foreign key relationship
        builder.HasOne(cm => cm.Conversation)
            .WithMany(cc => cc.Messages)
            .HasForeignKey(cm => cm.ConversationId)
            .OnDelete(DeleteBehavior.Cascade);

        // Index for efficient querying
        builder.HasIndex(cm => cm.ConversationId);
    }
}
