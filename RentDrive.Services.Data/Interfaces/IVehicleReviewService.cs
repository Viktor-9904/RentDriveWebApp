namespace RentDrive.Services.Data.Interfaces
{
    public interface IVehicleReviewService
    {
        public Task<double> GetVehicleStarRatingByIdAsync(Guid vehicleId);
    }
}
