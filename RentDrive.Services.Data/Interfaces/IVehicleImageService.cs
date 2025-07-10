using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IVehicleImageService
    {
        public Task<string> GetFirstImageByVehicleIdAsync(Guid vehicleId);
        public Task<List<string>> GetAllImagesByVehicleIdAsync(Guid id);
        public Task<bool> AddImagesAsync(List<IFormFile> images, Guid vehicleId);

    }
}
