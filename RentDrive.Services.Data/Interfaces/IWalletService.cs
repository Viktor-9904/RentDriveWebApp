using RentDrive.Web.ViewModels.WalletTransaction;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IWalletService
    {
        public Task<WalletTransactionHistoryViewModel?> AddFundsAsync(string userId, AddFundsViewModel addFundsViewModel);
    }
}
