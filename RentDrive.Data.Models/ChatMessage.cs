namespace RentDrive.Data.Models
{
    public class ChatMessage
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public ApplicationUser Sender { get; set; } = null!;
        public Guid ReceiverId { get; set; }
        public ApplicationUser Receiver { get; set; } = null!;
        public DateTime TimeSent { get; set; } = DateTime.UtcNow;
        public string Text { get; set; } = null!;
    }
}
