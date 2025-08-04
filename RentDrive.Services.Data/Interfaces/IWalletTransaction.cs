using RentDrive.Web.ViewModels.WalletTransaction;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IWalletTransaction
    {
        public Task<IEnumerable<WalletTransactionHistoryViewModel>> GetWalletTransactionHistoryByUserIdAsync(string userId);
    }
}
