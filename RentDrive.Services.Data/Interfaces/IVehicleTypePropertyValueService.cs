using RentDrive.Web.ViewModels.Vehicle;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IVehicleTypePropertyValueService
    {
        Task<List<VehicleTypePropertyValuesViewModel>> GetVehicleTypePropertyValuesByVehicleIdAsync(Guid vehicleId);
    }
}
