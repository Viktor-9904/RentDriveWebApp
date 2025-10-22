using RentDrive.Services.Data.Common;
using RentDrive.Web.ViewModels.Vehicle;
using RentDrive.Web.ViewModels.VehicleTypePropertyValue;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IVehicleTypePropertyValueService
    {
        Task<ServiceResponse<List<VehicleTypePropertyValuesViewModel>>> GetVehicleTypePropertyValuesByVehicleIdAsync(Guid vehicleId);
        Task<ServiceResponse<bool>> AddVehicleTypePropertyValuesAsync(Guid vehicleId, IEnumerable<VehicleTypePropertyValueInputViewModel> submittedPropertyValues);
        Task<ServiceResponse<bool>> UpdateVehicleTypePropertyValuesAsync(Guid vehicleId, IEnumerable<VehicleTypePropertyValueInputViewModel> submittedPropertyValues);
        Task<ServiceResponse<FilterTypeProperties?>> LoadTypePropertyValuesByTypeIdAsync(int vehicleTypeId);
    }
}
