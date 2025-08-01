namespace RentDrive.Data.Models
{
    public class Wallet
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;
        public decimal Balance { get; set; }
        public ICollection<WalletTransaction> Transactions { get; set; }
            = new List<WalletTransaction>();
    }
}
