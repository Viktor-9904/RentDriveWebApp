using RentDrive.Web.ViewModels;
using RentDrive.Web.ViewModels.Vehicle;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<RecentVehicleIndexViewModel>> IndexGetTop3RecentVehiclesAsync();
        Task<IEnumerable<ListingVehicleViewModel>> GetAllVehiclesAsync();
        Task<VehicleDetailsViewModel?> GetVehicleDetailsByIdAsync(Guid id);
        Task<bool> CreateVehicle(VehicleCreateFormViewModel viewModel);
        Task<bool> SoftDeleteVehicleByIdAsync(Guid id);
    }
}
