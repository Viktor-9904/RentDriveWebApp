using System.Globalization;

using Microsoft.EntityFrameworkCore;

using RentDrive.Common.Enums;
using RentDrive.Data;
using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Common;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.Rental;

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
        public async Task<ServiceResponse<IEnumerable<DateTime>>> GetBookedDatesByVehicleIdAsync(Guid vehicleId)
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

            return ServiceResponse<IEnumerable<DateTime>>.Ok(bookedDates);
        }

        public async Task<ServiceResponse<bool>> RentVehicle(Guid vehicleId, Guid renterId, IEnumerable<DateTime> bookedDates)
        {
            ServiceResponse<bool> areDatesValidResponse = await AreDatesValid(vehicleId, bookedDates);

            if (!areDatesValidResponse.Success)
            {
                return ServiceResponse<bool>.Fail("Invalid Selected Dates!");
            }

            List<DateTime> orderedDates = bookedDates
                .Select(d => d.Date)
                .OrderBy(d => d)
                .ToList();

            ApplicationUser? renter = await this.applicationUserRepository
                .GetAllAsQueryable()
                .Include(au => au.Wallet)
                .FirstOrDefaultAsync(au => au.Id == renterId);

            if (renter == null || renter?.Wallet == null)
            {
                return ServiceResponse<bool>.Fail("Current User Not Found!");
            }

            Vehicle? vehicle = await this.vehicleRepository
                .GetAllAsQueryable()
                .Include(v => v.Owner)
                .ThenInclude(au => au.Wallet)
                .FirstOrDefaultAsync(v => v.Id == vehicleId);

            if (vehicle == null)
            {
                return ServiceResponse<bool>.Fail("Rent Vehicle Not Found!");
            }

            if (renterId == vehicle.OwnerId)
            {
                return ServiceResponse<bool>.Fail("You Can't Rent Your Own Vehicle!");
            }

            if (vehicle.Owner == null || vehicle.Owner.Wallet == null)
            {
                return ServiceResponse<bool>.Fail("Vehicle Owner Not Found!");
            }

            decimal totalRentalPrice = vehicle.PricePerDay * orderedDates.Count;

            if (renter.Wallet.Balance < totalRentalPrice)
            {
                return ServiceResponse<bool>.Fail("Insufficient Balance");
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
                return ServiceResponse<bool>.Ok(true); ;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<ServiceResponse<bool>> AreDatesValid(Guid vehicleId, IEnumerable<DateTime> dates)
        {
            if (dates == null || !dates.Any())
            {
                return ServiceResponse<bool>.Fail("No Dates Selected!");
            }

            List<DateTime> requested = dates
                .Select(d => d.Date)
                .OrderBy(d => d)
                .ToList();

            ServiceResponse<IEnumerable<DateTime>> alreadyBookedDatesResponse = await GetBookedDatesByVehicleIdAsync(vehicleId);

            HashSet<DateTime> bookedDatesSet = alreadyBookedDatesResponse.Result!
                .Select(d => d.Date)
                .ToHashSet();

            if (requested.Any(bookedDatesSet.Contains))
            {
                return ServiceResponse<bool>.Fail("Already Booked Dates Selected!");
            }

            for (int i = 1; i < requested.Count; i++) // check for gaps
            {
                if ((requested[i] - requested[i - 1]).Days != 1)
                {
                    return ServiceResponse<bool>.Fail("Rental Dates Are Not Consecutive!");
                }
            }

            return ServiceResponse<bool>.Ok(true);
        }

        public async Task<ServiceResponse<int>> GetCompletedRentalsCountByUserIdAsync(Guid userId)
        {
            int completedRentalsCount = await this.rentalRepository
                .GetAllAsQueryable()
                .Where(r =>
                    r.RenterId == userId &&
                    r.Status == RentalStatus.Completed)
                .CountAsync();

            return ServiceResponse<int>.Ok(completedRentalsCount);
        }

        public async Task<ServiceResponse<IEnumerable<UserRentalViewModel>>> GetUserRentalsByIdAsync(Guid userId)
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

            return ServiceResponse<IEnumerable<UserRentalViewModel>>.Ok(userRentals);
        }

        public async Task<ServiceResponse<bool>> ConfirmRentalByIdAsync(Guid userId, Guid rentalId)
        {
            Rental? rental = await this.rentalRepository
                .GetAllAsQueryable()
                .FirstOrDefaultAsync(r =>
                    r.Id == rentalId &&
                    r.RenterId == userId &&
                    r.Status != RentalStatus.Completed);

            if (rental == null)
            {
                return ServiceResponse<bool>.Fail("Rental Not Found!");
            }

            Vehicle vehicle = await this.vehicleRepository
                .GetByIdAsync(rental.VehicleId);

            if (vehicle == null)
            {
                return ServiceResponse<bool>.Fail("Vehicle Not Found!");
            }

            ApplicationUser? owner = await this.applicationUserRepository
                .GetAllAsQueryable()
                .Include(au => au.Wallet)
                .FirstOrDefaultAsync(au => au.Id == vehicle.OwnerId);

            if (owner == null || owner?.Wallet == null)
            {
                return ServiceResponse<bool>.Fail("Rental Owner Not Found!");
            }

            int daysRented = (rental.EndDate - rental.StartDate).Days + 1;
            decimal totalRentalPrice = rental.VehiclePricePerDay * daysRented;

            using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                if (vehicle.OwnerId == new Guid(CompanyId)) // Vehicle is company ownder
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
                        .FirstOrDefaultAsync(au => au.Id == new Guid(CompanyId));

                    if (company == null || company?.Wallet == null)
                    {
                        return ServiceResponse<bool>.Fail("Comapny Not Found!");
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
                return ServiceResponse<bool>.Ok(true);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<ServiceResponse<bool>> CancelRentalByIdAsync(Guid userId, Guid rentalId)
        {
            Rental? rental = await this.rentalRepository
                .GetAllAsQueryable()
                .Include(r => r.Renter)
                .ThenInclude(au => au.Wallet)
                .FirstOrDefaultAsync(r =>
                    r.Id == rentalId &&
                    r.RenterId == userId &&
                    r.StartDate > DateTime.UtcNow &&
                    r.Status == RentalStatus.Active);

            if (rental == null)
            {
                return ServiceResponse<bool>.Fail("Rental Not Found!");
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


                return ServiceResponse<bool>.Ok(true);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<ServiceResponse<IEnumerable<UserVehicleRentalViewModel>>> GetUserOwnedVehiclesRentals(Guid userId, Guid vehicleId)
        {
            IEnumerable<UserVehicleRentalViewModel> userVehicleRentals = await this.rentalRepository
                .GetAllAsQueryable()
                .Include(r => r.Vehicle)
                .Include(r => r.Renter)
                .Where(r =>
                    r.Vehicle.OwnerId == userId &&
                    r.VehicleId == vehicleId)
                .Select(r => new UserVehicleRentalViewModel()
                {
                    Id = r.Id,
                    Username = r.Renter.UserName ?? "Unknown",
                    BookedOn = $"{r.BookedOn.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}",
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    Period = $"{r.StartDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)} - {r.EndDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}",
                    Status = r.Status.ToString(),
                    PricePerDay = r.VehiclePricePerDay,
                    TotalPrice = r.TotalPrice,
                })
                .ToListAsync();

            return ServiceResponse<IEnumerable<UserVehicleRentalViewModel>>.Ok(userVehicleRentals);
        }
    }
}
