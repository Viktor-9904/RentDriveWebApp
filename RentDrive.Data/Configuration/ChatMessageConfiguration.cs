using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RentDrive.Data.Models;
using System.Reflection.Emit;
using static RentDrive.Common.EntityValidationConstants.ChatMessageConstants;

namespace RentDrive.Data.Configuration
{
    public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder
                .HasKey(cm => cm.Id);

            builder
                .HasOne(cm => cm.Sender)
                .WithMany(au => au.SentMessages)
                .HasForeignKey(cm => cm.SenderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(cm => cm.Receiver)
                .WithMany(au => au.ReceivedMessages)
                .HasForeignKey(cm => cm.ReceiverId)
                .OnDelete(DeleteBehavior.NoAction);


            builder
                .Property(cm => cm.TimeSent)
                .IsRequired()
                .HasComment("Message time sent.");

            builder
                .Property(cm => cm.Message)
                .IsRequired()
                .HasComment("Message sent.")
                .HasMaxLength(ChatMessageMaxLength);

            builder
                .HasIndex(cm => new { cm.SenderId, cm.ReceiverId });
        }
    }
}
