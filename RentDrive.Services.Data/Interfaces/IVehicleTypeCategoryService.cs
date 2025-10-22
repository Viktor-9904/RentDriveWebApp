using RentDrive.Services.Data.Common;
using RentDrive.Web.ViewModels.VehicleTypeCategory;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IVehicleTypeCategoryService
    {
        public Task<ServiceResponse<IEnumerable<VehicleTypeCategoryViewModel>>> GetAllCategories();
        public Task<ServiceResponse<bool>> DeleteByIdAsync(Guid userId, int id);
        public Task<ServiceResponse<VehicleTypeCategoryEditFormViewModel?>> EditCategory(Guid userId, VehicleTypeCategoryEditFormViewModel viewModel);
        public Task<ServiceResponse<VehicleTypeCategoryCreateFormViewModel?>> CreateCategory(Guid userId, VehicleTypeCategoryCreateFormViewModel viewModel);
    }
}
