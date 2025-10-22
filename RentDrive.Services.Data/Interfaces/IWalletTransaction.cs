using RentDrive.Services.Data.Common;
using RentDrive.Web.ViewModels.WalletTransaction;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IWalletTransaction
    {
        public Task<ServiceResponse<IEnumerable<WalletTransactionHistoryViewModel>>> GetWalletTransactionHistoryByUserIdAsync(Guid userId);
    }
}
