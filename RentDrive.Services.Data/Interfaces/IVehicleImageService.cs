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
    }
}
