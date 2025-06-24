using RentDrive.Web.ViewModels.Vehicles;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IVehicleTypePropertyService
    {
        Task<IEnumerable<VehicleTypePropertyViewModel>> GetAllVehicleTypePropertiesAsync();
    }
}
