using System.ComponentModel.DataAnnotations;

using static RentDrive.Common.EntityValidationConstants.ChatMessageConstants;

namespace RentDrive.Web.ViewModels.Chat
{
    public class SentChatMessgeViewModel
    {
        [Required]
        public Guid ReceiverId { get; set; }
        [Required]
        [MaxLength(ChatMessageMaxLength)]
        public string Text { get; set; } = null!;

    }
}
