namespace RentDrive.Web.ViewModels.WalletTransaction
{
    public class WalletTransactionHistoryViewModel
    {
        public Guid Id { get; set; }
        public string CreatedOn { get; set; } = null!;
        public decimal Amount { get; set; }
        public string Type { get; set; } = null!;
    }
}
