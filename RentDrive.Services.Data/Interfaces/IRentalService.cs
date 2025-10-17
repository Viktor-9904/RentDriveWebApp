using Microsoft.AspNetCore.Identity;
using RentDrive.Data.Models;
using RentDrive.Web.ViewModels.Rental;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IRentalService
    {
        public Task<IEnumerable<DateTime>> GetBookedDatesByVehicleIdAsync(Guid vehicleId);
        public Task<bool> RentVehicle(Guid vehicleId, Guid renterId, IEnumerable<DateTime> bookedDates);
        public Task<bool> AreDatesValid(Guid vehicleId, IEnumerable<DateTime> bookedDates);
        public Task<int> GetCompletedRentalsCountByUserIdAsync(Guid userId);
        public Task<IEnumerable<UserRentalViewModel>> GetUserRentalsByIdAsync(Guid userId);
        public Task<bool> ConfirmRentalByIdAsync(string userId, Guid rentalId);
        public Task<bool> CancelRentalByIdAsync(string userId, Guid rentalId);
        public Task<IEnumerable<UserVehicleRentalViewModel>> GetUserOwnedVehiclesRentals(string userId, Guid vehicleId);
    }
}
