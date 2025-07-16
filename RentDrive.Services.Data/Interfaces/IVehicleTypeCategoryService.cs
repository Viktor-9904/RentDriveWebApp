using RentDrive.Web.ViewModels.VehicleTypeCategory;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IVehicleTypeCategoryService
    {
        public Task<IEnumerable<VehicleTypeCategoryViewModel>> GetAllCategories();
        public Task<bool> DeleteByIdAsync(int id);
        public Task<VehicleTypeCategoryEditFormViewModel?> EditCategory(VehicleTypeCategoryEditFormViewModel viewModel);
        public Task<VehicleTypeCategoryCreateFormViewModel?> CreateCategory(VehicleTypeCategoryCreateFormViewModel viewModel);
    }
}
