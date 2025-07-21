namespace RentDrive.Services.Data.Interfaces
{
    public interface IRentalService
    {
        public Task<IEnumerable<DateTime>> GetBookedDatesByVehicleIdAsync(Guid vehicleId);
        public Task<bool> RentVehicle(Guid vehicleId, Guid RenterId, IEnumerable<DateTime> bookedDates);
        public Task<bool> AreDatesValid(Guid vehicleId, IEnumerable<DateTime> bookedDates);
    }
}
