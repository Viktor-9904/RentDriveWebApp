using RentDrive.Web.ViewModels.Vehicle;
using RentDrive.Web.ViewModels.VehicleTypeProperty;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IVehicleTypePropertyValueService
    {
        Task<List<VehicleTypePropertyValuesViewModel>> GetVehicleTypePropertyValuesByVehicleIdAsync(Guid vehicleId);
    }
}
