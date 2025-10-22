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
    public class WalletService : IWalletService
    {
        private readonly IRepository<Wallet, Guid> walletRepository;
        private readonly IRepository<WalletTransaction, Guid> walletTransactionRepository;

        public WalletService(
            IRepository<Wallet, Guid> walletRepository,
            IRepository<WalletTransaction, Guid> walletTransactionRepository)
        {
            this.walletRepository = walletRepository;
            this.walletTransactionRepository = walletTransactionRepository;
        }

        public async Task<ServiceResponse<WalletTransactionHistoryViewModel?>> AddFundsAsync(string userId, AddFundsViewModel addFundsViewModel)
        {
            Wallet? userWallet = await this.walletRepository
                .GetAllAsQueryable()
                .FirstOrDefaultAsync(w => w.UserId.ToString() == userId);

            if (userWallet == null)
            {
                return ServiceResponse<WalletTransactionHistoryViewModel?>.Fail("User Wallet Not Found!");
            }

            WalletTransaction transaction = new WalletTransaction()
            {
                WalletId = userWallet.Id,
                Amount = addFundsViewModel.Amount,
                Type = WalletTransactionType.Deposit,
                CreatedAt = DateTime.UtcNow,
            };


            await this.walletTransactionRepository.AddAsync(transaction);

            userWallet.Balance += addFundsViewModel.Amount;

            await this.walletTransactionRepository.SaveChangesAsync();


            return ServiceResponse<WalletTransactionHistoryViewModel?>.Ok(
                new WalletTransactionHistoryViewModel()
                {
                    Id = transaction.Id,
                    CreatedOn = $"{transaction.CreatedAt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}",
                    Amount = transaction.Amount,
                    Type = WalletTransactionType.Deposit.GetDescription(),
                }
            );
        }
    }
}
