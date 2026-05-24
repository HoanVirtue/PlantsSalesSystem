using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlantSales.API.Domain.Entities;

namespace PlantSales.API.Infrastructure.Data.Configurations;

public class ChatConversationConfiguration : BaseEntityConfiguration<ChatConversation>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ChatConversation> builder)
    {
        builder.Property(cc => cc.SessionId)
            .HasMaxLength(255);

        builder.Property(cc => cc.StartedAt);

        builder.Property(cc => cc.EndedAt);

        // Foreign key relationship (optional - Customer)
        builder.HasOne(cc => cc.Customer)
            .WithMany(c => c.ChatConversations)
            .HasForeignKey(cc => cc.CustomerId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        // Collection relationship
        builder.HasMany(cc => cc.Messages)
            .WithOne(cm => cm.Conversation)
            .HasForeignKey(cm => cm.ConversationId)
            .OnDelete(DeleteBehavior.Cascade);

        // Index for efficient querying
        builder.HasIndex(cc => cc.SessionId);
    }
}
