using System.ComponentModel.DataAnnotations;

using static RentDrive.Common.EntityValidationConstants.ChatMessageConstants;
 
namespace RentDrive.Web.ViewModels.Chat
{
    public class ChatMessageViewModel
    {
        [Required]
        public Guid SenderId { get; set; }
        [Required]
        public Guid ReceiverId { get; set; }

        [Required]
        [MaxLength(ChatMessageMaxLength)]
        public string Text { get; set; } = null!;

        [Required]
        public DateTime TimeSent { get; set; }
    }
}
