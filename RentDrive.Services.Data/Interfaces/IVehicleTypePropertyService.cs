using RentDrive.Web.ViewModels.VehicleTypeProperty;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IVehicleTypePropertyService
    {
        Task<IEnumerable<VehicleTypePropertyViewModel>> GetAllVehicleTypePropertiesAsync();
    }
}
