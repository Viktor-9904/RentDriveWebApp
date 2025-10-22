using Microsoft.AspNetCore.Http;

using RentDrive.Services.Data.Common;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IVehicleImageService
    {
        public Task<ServiceResponse<string>> GetFirstImageByVehicleIdAsync(Guid vehicleId);
        public Task<ServiceResponse<List<string>>> GetAllImagesByVehicleIdAsync(Guid id);
        public Task<ServiceResponse<bool>> AddImagesAsync(List<IFormFile> images, Guid vehicleId);

    }
}
