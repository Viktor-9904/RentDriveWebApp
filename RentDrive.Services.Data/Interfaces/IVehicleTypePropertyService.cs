using RentDrive.Web.ViewModels.VehicleTypeProperty;
using RentDrive.Web.ViewModels.VehicleTypePropertyValue;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IVehicleTypePropertyService
    {
        Task<IEnumerable<VehicleTypePropertyViewModel>> GetAllVehicleTypePropertiesAsync();
        Task<bool> CreateVehicleTypeProperty(CreateVehicleTypePropertyViewModel viewModel);
        Task<bool> EditPropertyAsync(EditVehicleTypePropertyViewModel viewModel);
        Task<bool> DeletePropertyByIdAsync(Guid id);
        Task<bool> ValidateVehicleTypeProperties(int vehicleTypeId, IEnumerable<CreateFormVehicleTypePropertyValueViewModel> propertyValues);
    }
}