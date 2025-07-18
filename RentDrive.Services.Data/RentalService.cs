using Microsoft.EntityFrameworkCore;
using RentDrive.Common.Enums;
using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Interfaces;

namespace RentDrive.Services.Data
{
    public class RentalService : IRentalService
    {
        private readonly IRepository<Rental, Guid> rentalRepository;

        public RentalService(IRepository<Rental, Guid> rentalRepository)
        {
            this.rentalRepository = rentalRepository;
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
    }
}
