using Microsoft.EntityFrameworkCore;

using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.VehicleTypeCategory;

namespace RentDrive.Services.Data
{
    public class VehicleTypeCategoryService : IVehicleTypeCategoryService
    {

        private readonly IRepository<VehicleTypeCategory, int> vehicleTypeCategoryRepository;
        private readonly IRepository<Vehicle, Guid> vehicleRepository;
        public VehicleTypeCategoryService(
            IRepository<VehicleTypeCategory, int> vehicleTypeCategoryRepository,
            IRepository<Vehicle, Guid> vehicleRepository)
        {
            this.vehicleTypeCategoryRepository = vehicleTypeCategoryRepository;
            this.vehicleRepository = vehicleRepository;
        }
        public async Task<IEnumerable<VehicleTypeCategoryViewModel>> GetAllCategories()
        {
            IEnumerable<VehicleTypeCategoryViewModel> allCategories = await this.vehicleTypeCategoryRepository
                .GetAllAsQueryable()
                .Where(vtc => vtc.IsDeleted == false)
                .Select(vtc => new VehicleTypeCategoryViewModel()
                {
                    Id = vtc.Id,
                    VehicleTypeId = vtc.VehicleTypeId,
                    Name = vtc.Name,
                    Description = vtc.Description
                })
                .ToListAsync();

            return allCategories;
        }
        public async Task<bool> DeleteByIdAsync(int id)
        {
            VehicleTypeCategory? category = await this.vehicleTypeCategoryRepository
                .GetAllAsQueryable()
                .FirstOrDefaultAsync(vtc =>
                    vtc.Id == id &&
                    vtc.IsDeleted == false);

            if (category == null)
            {
                return false;
            }

            bool currentlyInUse = await this.vehicleRepository
                .GetAllAsQueryable()
                .AnyAsync(v => v.VehicleTypeCategoryId == id);

            if (currentlyInUse)
            {
                return false;
            }

            category.IsDeleted = true;
            await this.vehicleTypeCategoryRepository.SaveChangesAsync();

            return true;
        }

        public async Task<VehicleTypeCategoryEditFormViewModel?> EditCategory(VehicleTypeCategoryEditFormViewModel viewModel)
        {
            VehicleTypeCategory? category = await this.vehicleTypeCategoryRepository
                .GetAllAsQueryable()
                .FirstOrDefaultAsync(vtc =>
                    vtc.Id == viewModel.Id &&
                    vtc.IsDeleted == false);

            if (category == null)
            {
                return null;
            }

            category.Name = viewModel.Name;
            category.Description = viewModel.Description;

            await this.vehicleRepository.SaveChangesAsync();

            return new VehicleTypeCategoryEditFormViewModel()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                VehicleTypeId = category.VehicleTypeId,
            };
        }

        public async Task<VehicleTypeCategoryCreateFormViewModel?> CreateCategory(VehicleTypeCategoryCreateFormViewModel viewModel)
        {
            bool alreadyExists = await this.vehicleTypeCategoryRepository
                .GetAllAsQueryable()
                .AnyAsync(vtc =>
                    vtc.Id == viewModel.Id &&
                    vtc.IsDeleted == false);

            if (alreadyExists)
            {
                return null;
            }

            VehicleTypeCategory newCategory = new VehicleTypeCategory()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                VehicleTypeId = viewModel.VehicleTypeId,
            };

            await this.vehicleTypeCategoryRepository.AddAsync(newCategory);
            await this.vehicleTypeCategoryRepository.SaveChangesAsync();

            return new VehicleTypeCategoryCreateFormViewModel()
            {
                Id = newCategory.Id,
                Name = newCategory.Name,
                Description = newCategory.Description,
                VehicleTypeId = newCategory.VehicleTypeId,
            };
        }
    }
}
