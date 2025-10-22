using RentDrive.Services.Data.Common;
using RentDrive.Web.ViewModels.VehicleTypeProperty;
using RentDrive.Web.ViewModels.VehicleTypePropertyValue;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IVehicleTypePropertyService
    {
        Task<ServiceResponse<IEnumerable<VehicleTypePropertyViewModel>>> GetAllVehicleTypePropertiesAsync();
        Task<ServiceResponse<VehicleTypePropertyViewModel>> CreateVehicleTypeProperty(Guid userId, CreateVehicleTypePropertyViewModel viewModel);
        Task<ServiceResponse<bool>> EditPropertyAsync(Guid userId, EditVehicleTypePropertyViewModel viewModel);
        Task<ServiceResponse<bool>> DeletePropertyByIdAsync(Guid userId, Guid id);
        Task<ServiceResponse<bool>> ValidateVehicleTypeProperties(int vehicleTypeId, IEnumerable<VehicleTypePropertyValueInputViewModel> propertyValues);
    }
}