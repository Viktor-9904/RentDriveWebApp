using RentDrive.Common.Enums;

namespace RentDrive.Data.Models
{
    public class WalletTransaction
    {
        public Guid Id { get; set; }
        public Guid WalletId { get; set; }
        public Wallet Wallet { get; set; } = null!;
        public decimal Amount { get; set; }
        public WalletTransactionType Type { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
