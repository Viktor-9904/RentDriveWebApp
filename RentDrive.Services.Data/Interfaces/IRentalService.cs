using RentDrive.Services.Data.Common;
using RentDrive.Web.ViewModels.Rental;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IRentalService
    {
        public Task<ServiceResponse<IEnumerable<DateTime>>> GetBookedDatesByVehicleIdAsync(Guid vehicleId);
        public Task<ServiceResponse<bool>> RentVehicle(Guid vehicleId, Guid renterId, IEnumerable<DateTime> bookedDates);
        public Task<ServiceResponse<bool>> AreDatesValid(Guid vehicleId, IEnumerable<DateTime> bookedDates);
        public Task<ServiceResponse<int>> GetCompletedRentalsCountByUserIdAsync(Guid userId);
        public Task<ServiceResponse<IEnumerable<UserRentalViewModel>>> GetUserRentalsByIdAsync(Guid userId);
        public Task<ServiceResponse<bool>> ConfirmRentalByIdAsync(Guid userId, Guid rentalId);
        public Task<ServiceResponse<bool>> CancelRentalByIdAsync(Guid userId, Guid rentalId);
        public Task<ServiceResponse<IEnumerable<UserVehicleRentalViewModel>>> GetUserOwnedVehiclesRentals(Guid userId, Guid vehicleId);
    }
}
