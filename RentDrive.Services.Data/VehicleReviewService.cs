using Microsoft.EntityFrameworkCore;

using RentDrive.Common.Enums;
using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Common;
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

        public async Task<ServiceResponse<int>> GetVehicleReviewCountByIdAsync(Guid vehicleId)
        {
            int reviewCount = await this.vehicleReviewRepository
                .GetAllAsQueryable()
                .Where(vr => vr.VehicleId == vehicleId)
                .CountAsync();

            return ServiceResponse<int>.Ok(reviewCount);
        }

        public async Task<ServiceResponse<double>> GetVehicleStarRatingByIdAsync(Guid vehicleId)
        {
            double averageRating = await this.vehicleReviewRepository
                .GetAllAsQueryable()
                .Where(vr => vr.VehicleId == vehicleId)
                .Select(vr => (double?)vr.Stars)
                .AverageAsync() ?? 0;

            return ServiceResponse<double>.Ok(averageRating);
        }

        public async Task<ServiceResponse<bool>> AddVehicleReview(Guid userId, AddNewReviewViewModel viewModel)
        {
            Rental? rentalToReview = await this.rentalRepository
                .GetAllAsQueryable()
                .Include(r => r.Review)
                .FirstOrDefaultAsync(r =>
                    r.RenterId == userId &&
                    r.Id == viewModel.RentalId);

            if (rentalToReview == null)
            {
                return ServiceResponse<bool>.Fail("Rental Not Found!");
            }

            if (rentalToReview.Status != RentalStatus.Completed)
            {
                return ServiceResponse<bool>.Fail("Can't Review An Active Rental!");
            }

            if (rentalToReview.Review != null)
            {
                return ServiceResponse<bool>.Fail("Rental Is Already Reviewed!");
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

            return ServiceResponse<bool>.Ok(true);
        }
    }
}
