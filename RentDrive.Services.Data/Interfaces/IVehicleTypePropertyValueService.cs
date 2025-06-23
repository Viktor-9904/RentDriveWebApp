using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IVehicleTypePropertyValueService
    {
        Task<IEnumerable<VehicleTypePropertyValue>> GetVehicleTypePropertyValuesByVehicleIdAsync(Guid vehicleId);
    }
}
