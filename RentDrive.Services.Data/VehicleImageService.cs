using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Interfaces;

using static RentDrive.Common.Vehicle.VehicleValidationConstants.VehicleImages;

namespace RentDrive.Services.Data
{
    public class VehicleImageService : IVehicleImageService
    {
        private readonly IRepository<VehicleImages, Guid> vehicleImageRepository;

        public VehicleImageService(IRepository<VehicleImages, Guid> vehicleImageRepository)
        {
            this.vehicleImageRepository = vehicleImageRepository;
        }
        public async Task<string> GetFirstImageByVehicleIdAsync(Guid vehicleId)
        {
            string? firstImageURL = await this.vehicleImageRepository
                .GetAllAsQueryable()
                .Where(vi => vi.VehicleId == vehicleId)
                .Select(vi => vi.ImageURL)
                .FirstOrDefaultAsync();

            return firstImageURL.IsNullOrEmpty() ? DefaultImageURL : firstImageURL!;
        }
    }
}
