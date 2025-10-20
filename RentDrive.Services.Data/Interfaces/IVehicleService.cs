using RentDrive.Web.ViewModels;
using RentDrive.Web.ViewModels.Vehicle;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<RecentVehicleIndexViewModel>> IndexGetTop3RecentVehiclesAsync();
        Task<IEnumerable<ListingVehicleViewModel>> GetAllVehiclesAsync();
        Task<VehicleDetailsViewModel?> GetVehicleDetailsByIdAsync(Guid id);
        Task<VehicleEditFormViewModel?> GetEditVehicleDetailsByIdAsync(Guid editorId, Guid vehicleId);
        Task<bool> CreateVehicle(string userId, VehicleCreateFormViewModel viewModel);
        Task<bool> UpdateVehicle(Guid editorId, VehicleEditFormViewModel viewModel);
        Task<bool> SoftDeleteVehicleByIdAsync(Guid deletedByUserId,Guid vehicleId);
        Task<decimal> GetVehiclePricePerDayByVehicleId(Guid id);
        Task<int> GetUserListedVehicleCountAsync(Guid userId);
        Task<IEnumerable<UserVehicleViewModel>> GetUserVehiclesByIdAsync(string userId);
        Task<BaseFilterProperties> GetBaseFilterPropertiesAsync(int? vehicleTypeId = null, int? vehicleTypeCategoryId = null);
        Task<IEnumerable<Guid>> GetFilteredVehicles(FilteredVehiclesViewModel filter);
        Task<IEnumerable<ListingVehicleViewModel>> GetSearchQueryVehicles(string searchQuery);
        Task<IEnumerable<string>> GetAllVehicleMakesAsync();
        Task<int> GetActiveListings();
    }
}
