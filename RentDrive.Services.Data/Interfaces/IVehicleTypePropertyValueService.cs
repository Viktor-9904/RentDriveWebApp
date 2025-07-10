using RentDrive.Web.ViewModels.Vehicle;
using RentDrive.Web.ViewModels.VehicleTypePropertyValue;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IVehicleTypePropertyValueService
    {
        Task<List<VehicleTypePropertyValuesViewModel>> GetVehicleTypePropertyValuesByVehicleIdAsync(Guid vehicleId);
        Task<bool> AddVehicleTypePropertyValuesAsync(Guid vehicleId, IEnumerable<CreateFormVehicleTypePropertyValueViewModel> submitterPropertyValues);
    }
}
