using Microsoft.EntityFrameworkCore;

using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Interfaces;

namespace RentDrive.Services.Data
{
    public class VehicleReviewService : IVehicleReviewService
    {
        private readonly IRepository<VehicleReview, Guid> vehicleReviewRepository;

        public VehicleReviewService(IRepository<VehicleReview, Guid> vehicleReviewRepository)
        {
            this.vehicleReviewRepository = vehicleReviewRepository;
        }

        public async Task<int> GetVehicleReviewCountByIdAsync(Guid vehicleId)
        {
            int reviewCount = await this.vehicleReviewRepository
                .GetAllAsQueryable()
                .Where(vr => vr.VehicleId == vehicleId)
                .CountAsync();

            return reviewCount;
        }

        public async Task<double> GetVehicleStarRatingByIdAsync(Guid vehicleId)
        {
            double averageRating = await this.vehicleReviewRepository
                .GetAllAsQueryable()
                .Where(vr => vr.VehicleId == vehicleId)
                .Select(vr => (double?)vr.Stars)
                .AverageAsync() ?? 0;

            return averageRating;
        }
    }
}
