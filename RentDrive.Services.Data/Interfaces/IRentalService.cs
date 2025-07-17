namespace RentDrive.Services.Data.Interfaces
{
    public interface IRentalService
    {
        public Task<IEnumerable<DateTime>> GetBookedDatesByVehicleIdAsync(Guid vehicleId);
    }
}
