using System.Globalization;

using Microsoft.EntityFrameworkCore;

using RentDrive.Common.Enums;
using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Common;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.WalletTransaction;

namespace RentDrive.Services.Data
{
    public class WalletTransactionService : IWalletTransaction
    {
        private readonly IRepository<WalletTransaction, Guid> walletTransactionRepository;

        public WalletTransactionService(IRepository<WalletTransaction, Guid> walletTransactionRepository)
        {
            this.walletTransactionRepository = walletTransactionRepository;
        }

        public async Task<ServiceResponse<IEnumerable<WalletTransactionHistoryViewModel>>> GetWalletTransactionHistoryByUserIdAsync(Guid userId)
        {
            IEnumerable<WalletTransactionHistoryViewModel> userTranasctions = await this.walletTransactionRepository
                .GetAllAsQueryable()
                .Include(wt => wt.Wallet)
                .Where(wt => wt.Wallet.UserId == userId)
                .OrderByDescending(wt => wt.CreatedAt)
                .Select(wt => new WalletTransactionHistoryViewModel()
                {
                    Id = wt.Id,
                    CreatedOn = $"{wt.CreatedAt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}",
                    Amount = wt.Amount,
                    Type = wt.Type.GetDescription(),
                })
                .ToListAsync();

            return ServiceResponse<IEnumerable<WalletTransactionHistoryViewModel>>.Ok(userTranasctions);
        }
    }
}