using Microsoft.EntityFrameworkCore;
using RentDrive.Common.Enums;
using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.Rental;

namespace RentDrive.Services.Data
{
    public class RentalService : IRentalService
    {
        private readonly IRepository<Rental, Guid> rentalRepository;
        private readonly IVehicleService vehicleService;

        public RentalService(
            IRepository<Rental, Guid> rentalRepository,
            IVehicleService vehicleService)
        {
            this.rentalRepository = rentalRepository;
            this.vehicleService = vehicleService;
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

        public async Task<bool> RentVehicle(Guid vehicleId, Guid RenterId, IEnumerable<DateTime> bookedDates)
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

            decimal vehiclePricePerDay = await this.vehicleService
                .GetVehiclePricePerDayByVehicleId(vehicleId);

            if (vehiclePricePerDay == 0)
            {
                return false;
            }

            Rental rental = new Rental()
            {
                VehicleId = vehicleId,
                RenterId = RenterId,
                BookedOn = DateTime.UtcNow,
                StartDate = orderedDates.First(),
                EndDate = orderedDates.Last(),
                VehiclePricePerDay = vehiclePricePerDay,
                TotalPrice = orderedDates.Count * vehiclePricePerDay,
                Status = RentalStatus.Active,
            };

            await this.rentalRepository.AddAsync(rental);
            await this.rentalRepository.SaveChangesAsync();

            return true;
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
                .Include(r => r.Vehicle)
                .ThenInclude(v => v.VehicleImages)
                .Where(r => r.RenterId == userId)
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
                    r.RenterId.ToString() == userId);

            if (rental == null)
            {
                return false;
            }

            rental.Status = RentalStatus.Completed;
            rental.CompletedOn = DateTime.UtcNow;

            await this.rentalRepository.SaveChangesAsync();

            return true;
        }
    }
}
