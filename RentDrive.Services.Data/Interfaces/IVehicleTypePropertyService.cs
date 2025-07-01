using RentDrive.Web.ViewModels.VehicleTypeProperty;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IVehicleTypePropertyService
    {
        Task<IEnumerable<VehicleTypePropertyViewModel>> GetAllVehicleTypePropertiesAsync();
        EnumOptionsViewModel GetEnumOptions();
        Task<bool> EditPropertyAsync(EditVehicleTypePropertyViewModel viewModel);
        Task<bool> DeletePropertyByIdAsync(Guid id);
    }
}
