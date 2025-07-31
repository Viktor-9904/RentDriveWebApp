using Microsoft.EntityFrameworkCore;
using RentDrive.Common.Enums;
using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.VehicleReview;

namespace RentDrive.Services.Data
{
    public class VehicleReviewService : IVehicleReviewService
    {
        private readonly IRepository<VehicleReview, Guid> vehicleReviewRepository;
        private readonly IRepository<Rental, Guid> rentalRepository;

        public VehicleReviewService(
            IRepository<VehicleReview, Guid> vehicleReviewRepository,
            IRepository<Rental, Guid> rentalRepository)
        {
            this.vehicleReviewRepository = vehicleReviewRepository;
            this.rentalRepository = rentalRepository;
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
        public async Task<bool> AddVehicleReview(string userId, AddNewReviewViewModel viewModel)
        {
            Rental? rentalToReview = await this.rentalRepository
                .GetAllAsQueryable()
                .Include(r => r.Review)
                .FirstOrDefaultAsync(r =>
                    r.RenterId.ToString() == userId &&
                    r.Id == viewModel.RentalId);

            if (rentalToReview == null)
            {
                return false; // Rental not found.
            }

            if (rentalToReview.Status != RentalStatus.Completed)
            {
                return false; // Cannot review a rental that is not completed.
            }

            if (rentalToReview.Review != null)
            {
                return false; // Rental is already reviewed.
            }

            VehicleReview newReview = new VehicleReview()
            {
                VehicleId = rentalToReview.VehicleId,
                RentalId = rentalToReview.Id,
                ReviewerId = rentalToReview.RenterId,
                Stars = viewModel.StarRating,
                Comment = viewModel.Comment,
            };

            await this.vehicleReviewRepository.AddAsync(newReview);
            await this.vehicleReviewRepository.SaveChangesAsync();

            return true;
        }
    }
}
