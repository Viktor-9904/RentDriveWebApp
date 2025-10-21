using RentDrive.Services.Data.Common;
using RentDrive.Web.ViewModels;
using RentDrive.Web.ViewModels.Vehicle;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IVehicleService
    {
        Task<ServiceResponse<IEnumerable<RecentVehicleIndexViewModel>>> IndexGetTop3RecentVehiclesAsync();
        Task<ServiceResponse<IEnumerable<ListingVehicleViewModel>>> GetAllVehiclesAsync();
        Task<ServiceResponse<VehicleDetailsViewModel?>> GetVehicleDetailsByIdAsync(Guid id);
        Task<ServiceResponse<VehicleEditFormViewModel?>> GetEditVehicleDetailsByIdAsync(Guid editorId, Guid vehicleId);
        Task<ServiceResponse<bool>> CreateVehicle(Guid userId, VehicleCreateFormViewModel viewModel);
        Task<ServiceResponse<bool>> UpdateVehicle(Guid editorId, VehicleEditFormViewModel viewModel);
        Task<ServiceResponse<bool>> SoftDeleteVehicleByIdAsync(Guid deletedByUserId,Guid vehicleId);
        Task<ServiceResponse<decimal>> GetVehiclePricePerDayByVehicleId(Guid id);
        Task<ServiceResponse<int>> GetUserListedVehicleCountAsync(Guid userId);
        Task<ServiceResponse<IEnumerable<UserVehicleViewModel>>> GetUserVehiclesByIdAsync(Guid userId);
        Task<ServiceResponse<BaseFilterProperties>> GetBaseFilterPropertiesAsync(int? vehicleTypeId = null, int? vehicleTypeCategoryId = null);
        Task<ServiceResponse<IEnumerable<Guid>>> GetFilteredVehicles(FilteredVehiclesViewModel filter);
        Task<ServiceResponse<IEnumerable<ListingVehicleViewModel>>> GetSearchQueryVehicles(string searchQuery);
        Task<ServiceResponse<IEnumerable<string>>> GetAllVehicleMakesAsync();
        Task<ServiceResponse<int>> GetActiveListings();
    }
}
