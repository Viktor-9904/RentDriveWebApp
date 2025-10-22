using RentDrive.Services.Data.Common;
using RentDrive.Web.ViewModels.VehicleReview;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IVehicleReviewService
    {
        public Task<ServiceResponse<double>> GetVehicleStarRatingByIdAsync(Guid vehicleId);
        public Task<ServiceResponse<int>> GetVehicleReviewCountByIdAsync(Guid vehicleId);
        public Task<ServiceResponse<bool>> AddVehicleReview(Guid userId, AddNewReviewViewModel viewModel);
    }
}
