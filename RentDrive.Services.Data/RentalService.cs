using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RentDrive.Common.Enums;
using RentDrive.Data;
using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.Rental;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Numerics;
using static RentDrive.Common.EntityValidationConstants.ApplicationUserValidationConstants.Company;
using static RentDrive.Common.EntityValidationConstants.RentalValidationConstans.Fees;

namespace RentDrive.Services.Data
{
    public class RentalService : IRentalService
    {
        private readonly RentDriveDbContext dbContext;
        private readonly IRepository<Rental, Guid> rentalRepository;
        private readonly IRepository<ApplicationUser, Guid> applicationUserRepository;
        private readonly IRepository<WalletTransaction, Guid> walletTransactionRepository;
        private readonly IRepository<Vehicle, Guid> vehicleRepository;

        public RentalService(
            RentDriveDbContext dbContext,
            IRepository<Rental, Guid> rentalRepository,
            IRepository<ApplicationUser, Guid> applicationUserRepository,
            IRepository<WalletTransaction, Guid> walletTransactionRepository,
            IRepository<Vehicle, Guid> vehicleRepository)
        {
            this.dbContext = dbContext;
            this.rentalRepository = rentalRepository;
            this.applicationUserRepository = applicationUserRepository;
            this.walletTransactionRepository = walletTransactionRepository;
            this.vehicleRepository = vehicleRepository;
        }
        public async Task<IEnumerable<DateTime>> GetBookedDatesByVehicleIdAsync(Guid vehicleId)
        {
            var rentals = await this.rentalRepository
                .GetAllAsQueryable()
                .Where(r =>
                    r.VehicleId == vehicleId &&
                    r.Status == RentalStatus.Active)
                .Select(r => new { r.StartDate, r.EndDate })
                .ToListAsync();

            HashSet<DateTime> bookedDates = new HashSet<DateTime>();

            foreach (var rental in rentals)
            {
                for (DateTime date = rental.StartDate.Date; date <= rental.EndDate.Date; date = date.AddDays(1))
                {
                    DateTime utcDate = DateTime.SpecifyKind(date, DateTimeKind.Utc);
                    bookedDates.Add(utcDate);
                }
            }

            return bookedDates.ToList();
        }

        public async Task<bool> RentVehicle(Guid vehicleId, string renterId, IEnumerable<DateTime> bookedDates)
        {
            bool areDatesValid = await AreDatesValid(vehicleId, bookedDates);

            if (!areDatesValid)
            {
                return false;
            }

            List<DateTime> orderedDates = bookedDates
                .Select(d => d.Date)
                .OrderBy(d => d)
                .ToList();

            ApplicationUser? renter = await this.applicationUserRepository
                .GetAllAsQueryable()
                .Include(au => au.Wallet)
                .FirstOrDefaultAsync(au => au.Id.ToString() == renterId);

            if (renter == null || renter?.Wallet == null)
            {
                return false;
            }

            Vehicle vehicle = await this.vehicleRepository
                .GetByIdAsync(vehicleId);

            if (vehicle == null)
            {
                return false;
            }

            ApplicationUser? owner = await this.applicationUserRepository
                .GetAllAsQueryable()
                .Include(au => au.Wallet)
                .FirstOrDefaultAsync(au => au.Id == vehicle.OwnerId);

            if (owner == null || owner?.Wallet == null)
            {
                return false;
            }

            decimal totalRentalPrice = vehicle.PricePerDay * orderedDates.Count;

            if (renter.Wallet.Balance < totalRentalPrice)
            {
                return false;
            }

            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                Rental rental = new Rental()
                {
                    VehicleId = vehicleId,
                    RenterId = renter.Id,
                    BookedOn = DateTime.UtcNow,
                    StartDate = orderedDates.First(),
                    EndDate = orderedDates.Last(),
                    VehiclePricePerDay = vehicle.PricePerDay,
                    TotalPrice = totalRentalPrice,
                    Status = RentalStatus.Active,
                };

                WalletTransaction renterTransaction = new WalletTransaction()
                {
                    WalletId = renter.Wallet.Id,
                    Amount = -totalRentalPrice,
                    Type = WalletTransactionType.Withdraw,
                };

                renter.Wallet.Balance -= totalRentalPrice;

                await this.walletTransactionRepository.AddAsync(renterTransaction);

                await this.rentalRepository.AddAsync(rental);
                await this.rentalRepository.SaveChangesAsync();

                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<bool> AreDatesValid(Guid vehicleId, IEnumerable<DateTime> dates)
        {
            if (dates == null || !dates.Any())
            {
                return false;
            }

            List<DateTime> requested = dates
                .Select(d => d.Date)
                .OrderBy(d => d)
                .ToList();

            IEnumerable<DateTime> alreadyBookedDates = await GetBookedDatesByVehicleIdAsync(vehicleId);

            HashSet<DateTime> bookedDatesSet = alreadyBookedDates
                .Select(d => d.Date)
                .ToHashSet();

            if (requested.Any(bookedDatesSet.Contains))
            {
                return false;
            }

            for (int i = 1; i < requested.Count; i++) // check for gaps
            {
                if ((requested[i] - requested[i - 1]).Days != 1)
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<int> GetCompletedRentalsCountByUserIdAsync(Guid userId)
        {
            int completedRentalsCount = await this.rentalRepository
                .GetAllAsQueryable()
                .Where(r =>
                    r.RenterId == userId &&
                    r.Status == RentalStatus.Completed)
                .CountAsync();

            return completedRentalsCount;
        }

        public async Task<IEnumerable<UserRentalViewModel>> GetUserRentalsByIdAsync(Guid userId)
        {
            List<UserRentalViewModel> userRentals = await this.rentalRepository
                .GetAllAsQueryable()
                .Include(r => r.Review)
                .Include(r => r.Vehicle)
                .ThenInclude(v => v.VehicleImages)
                .Where(r => r.RenterId == userId)
                .OrderByDescending(r => r.Status == RentalStatus.Active)
                .ThenBy(r => r.BookedOn)
                .Select(r => new UserRentalViewModel()
                {
                    Id = r.Id,
                    VehicleMake = r.Vehicle.Make,
                    VehicleModel = r.Vehicle.Model,
                    ImageUrl = r.Vehicle.VehicleImages.FirstOrDefault().ImageURL ?? "images/default-vehicle.jpg",
                    Status = r.Status.ToString(),
                    BookedOn = r.BookedOn,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    PricePerDay = r.VehiclePricePerDay,
                    TotalPrice = r.TotalPrice,
                    IsCompleted = r.Status == RentalStatus.Completed,
                    IsConfirmable = r.EndDate.Date < DateTime.UtcNow.Date,
                    IsCancelled = r.Status == RentalStatus.Cancelled,
                    IsCancellable = r.StartDate.Date > DateTime.UtcNow.Date,
                    HasReviewedVehicle = r.Review != null,
                })
                .ToListAsync();

            return userRentals;
        }

        public async Task<bool> ConfirmRentalByIdAsync(string userId, Guid rentalId)
        {
            Rental? rental = await this.rentalRepository
                .GetAllAsQueryable()
                .FirstOrDefaultAsync(r =>
                    r.Id == rentalId &&
                    r.RenterId.ToString() == userId &&
                    r.Status != RentalStatus.Completed);

            if (rental == null)
            {
                return false;
            }

            Vehicle vehicle = await this.vehicleRepository
                .GetByIdAsync(rental.VehicleId);

            if (vehicle == null)
            {
                return false;
            }

            ApplicationUser? owner = await this.applicationUserRepository
                .GetAllAsQueryable()
                .Include(au => au.Wallet)
                .FirstOrDefaultAsync(au => au.Id == vehicle.OwnerId);

            if (owner == null || owner?.Wallet == null)
            {
                return false;
            }

            int daysRented = (rental.EndDate - rental.StartDate).Days + 1;
            decimal totalRentalPrice = rental.VehiclePricePerDay * daysRented;

            using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                if (vehicle.OwnerId.ToString() == CompanyId) // Vehicle is company ownder
                {
                    WalletTransaction companyTransaction = new WalletTransaction()
                    {
                        WalletId = owner.Wallet.Id,
                        Amount = totalRentalPrice,
                        Type = WalletTransactionType.RentalProfit,
                    };

                    await this.walletTransactionRepository.AddAsync(companyTransaction);

                    owner.Wallet.Balance += totalRentalPrice;
                }
                else // Vehicle is owned by individual user
                {
                    ApplicationUser? company = await this.applicationUserRepository
                        .GetAllAsQueryable()
                        .Include(au => au.Wallet)
                        .FirstOrDefaultAsync(au => au.Id.ToString() == CompanyId);

                    if (company == null || company?.Wallet == null)
                    {
                        return false;
                    }

                    WalletTransaction ownerTransaction = new WalletTransaction()
                    {
                        WalletId = owner.Wallet.Id,
                        Amount = totalRentalPrice * (1 - CompanyPercentageFee),
                        Type = WalletTransactionType.RentalProfit
                    };

                    WalletTransaction companyTransaction = new WalletTransaction()
                    {
                        WalletId = company.Wallet.Id,
                        Amount = totalRentalPrice * CompanyPercentageFee,
                        Type = WalletTransactionType.CompanyRentalFeeProfit
                    };

                    await this.walletTransactionRepository.AddRangeAsync([ownerTransaction, companyTransaction]);

                    owner.Wallet.Balance += totalRentalPrice * (1 - CompanyPercentageFee);
                    company.Wallet.Balance += totalRentalPrice * CompanyPercentageFee;
                }

                rental.Status = RentalStatus.Completed;
                rental.CompletedOn = DateTime.UtcNow;

                await this.rentalRepository.SaveChangesAsync();

                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> CancelRentalByIdAsync(string userId, Guid rentalId)
        {
            Rental? rental = await this.rentalRepository
                .GetAllAsQueryable()
                .Include(r => r.Renter)
                .ThenInclude(au => au.Wallet)
                .FirstOrDefaultAsync(r =>
                    r.Id == rentalId &&
                    r.RenterId.ToString() == userId &&
                    r.StartDate > DateTime.UtcNow &&
                    r.Status == RentalStatus.Active);

            if (rental == null)
            {
                return false;
            }

            using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                WalletTransaction walletTransaction = new WalletTransaction()
                {
                    WalletId = rental.Renter.Wallet.Id,
                    Amount = rental.TotalPrice,
                    Type = WalletTransactionType.Refund,
                    CreatedAt = DateTime.UtcNow,
                };

                await this.walletTransactionRepository.AddAsync(walletTransaction);

                rental.Renter.Wallet.Balance += rental.TotalPrice;

                rental.Status = RentalStatus.Cancelled;
                rental.CancelledOn = DateTime.UtcNow;

                await this.rentalRepository.SaveChangesAsync();
                await transaction.CommitAsync();


                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<UserVehicleRentalViewModel>> GetUserOwnedVehiclesRentals(string userId, Guid vehicleId)
        {
            IEnumerable<UserVehicleRentalViewModel> userVehicleRentals = await this.rentalRepository
                .GetAllAsQueryable()
                .Include(r => r.Vehicle)
                .Include(r => r.Renter)
                .Where(r =>
                    r.Vehicle.OwnerId.ToString() == userId &&
                    r.VehicleId == vehicleId)
                .Select(r => new UserVehicleRentalViewModel()
                {
                    Id = r.Id,
                    Username = r.Renter.UserName,
                    BookedOn = $"{r.BookedOn.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}",
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    Period = $"{r.StartDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)} - {r.EndDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}",
                    Status = r.Status.ToString(),
                    PricePerDay = r.VehiclePricePerDay,
                    TotalPrice = r.TotalPrice,
                })
                .ToListAsync();

            return userVehicleRentals;
        }
    }
}
