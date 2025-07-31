using RentDrive.Web.ViewModels.VehicleReview;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IVehicleReviewService
    {
        public Task<double> GetVehicleStarRatingByIdAsync(Guid vehicleId);
        public Task<int> GetVehicleReviewCountByIdAsync(Guid vehicleId);
        public Task<bool> AddVehicleReview(string userId, AddNewReviewViewModel viewModel);
    }
}
