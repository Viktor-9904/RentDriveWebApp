using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Common;
using RentDrive.Services.Data.Interfaces;

using static RentDrive.Common.EntityValidationConstants.VehicleValidationConstants.VehicleImages;

namespace RentDrive.Services.Data
{
    public class VehicleImageService : IVehicleImageService
    {
        private readonly IRepository<VehicleImage, Guid> vehicleImageRepository;

        public VehicleImageService(IRepository<VehicleImage, Guid> vehicleImageRepository)
        {
            this.vehicleImageRepository = vehicleImageRepository;
        }
        public async Task<ServiceResponse<string>> GetFirstImageByVehicleIdAsync(Guid vehicleId)
        {
            string? firstImageURL = await this.vehicleImageRepository
                .GetAllAsQueryable()
                .Where(vi => vi.VehicleId == vehicleId)
                .Select(vi => vi.ImageURL)
                .FirstOrDefaultAsync();

            return ServiceResponse<string>.Ok(firstImageURL.IsNullOrEmpty() ? DefaultImageURL : firstImageURL!);
        }

        public async Task<ServiceResponse<List<string>>> GetAllImagesByVehicleIdAsync(Guid id)
        {
            List<string> allImages = await this.vehicleImageRepository
                .GetAllAsQueryable()
                .Where(vi => vi.VehicleId == id)
                .Select(vi => vi.ImageURL)
                .ToListAsync();

            if (allImages.Count == 0)
            {
                allImages.Add(DefaultImageURL);
            }

            return ServiceResponse<List<string>>.Ok(allImages);
        }

        public async Task<ServiceResponse<bool>> AddImagesAsync(List<IFormFile> images, Guid vehicleId)
        {
            if (images == null || images.Count == 0)
            {
                return ServiceResponse<bool>.Fail("No Images Provided!");
            }

            string wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string vehiclesImagesPath = Path.Combine(wwwrootPath, "images", "vehicles");

            if (!Directory.Exists(vehiclesImagesPath))
            {
                Directory.CreateDirectory(vehiclesImagesPath);
            }

            List<VehicleImage> vehicleImages = new List<VehicleImage>();

            foreach (var image in images)
            {
                string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                string filePath = Path.Combine(vehiclesImagesPath, uniqueFileName);

                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                VehicleImage vehicleImage = new VehicleImage
                {
                    VehicleId = vehicleId,
                    ImageURL = $"images/vehicles/{uniqueFileName}"
                };

                vehicleImages.Add(vehicleImage);
            }

            await vehicleImageRepository.AddRangeAsync(vehicleImages.ToArray());
            await vehicleImageRepository.SaveChangesAsync();

            return ServiceResponse<bool>.Ok(true);
        }
    }
}
