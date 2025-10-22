using RentDrive.Services.Data.Common;
using RentDrive.Web.ViewModels.WalletTransaction;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IWalletService
    {
        public Task<ServiceResponse<WalletTransactionHistoryViewModel?>> AddFundsAsync(string userId, AddFundsViewModel addFundsViewModel);
    }
}
