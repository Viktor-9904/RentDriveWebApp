using RentDrive.Web.ViewModels;
using RentDrive.Web.ViewModels.Vehicles;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<RecentVehicleIndexViewModel>> IndexGetTop3RecentVehiclesAsync();
        Task<IEnumerable<ListingVehicleViewModel>> GetAllVehiclesAsync();
        Task<VehicleDetailsViewModel?> GetVehicleDetailsByIdAsync(Guid id);
        Task<IEnumerable<VehicleTypeViewModel>> GetAllVehicleTypes();
        Task<IEnumerable<VehicleTypePropertyViewModel>> GetAllVehicleTypePropertiesAsync();
    }
}
