using Microsoft.EntityFrameworkCore;
using RentDrive.Common.Enums;
using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Common;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.VehicleTypeCategory;

namespace RentDrive.Services.Data
{
    public class VehicleTypeCategoryService : IVehicleTypeCategoryService
    {

        private readonly IRepository<VehicleTypeCategory, int> vehicleTypeCategoryRepository;
        private readonly IRepository<Vehicle, Guid> vehicleRepository;
        private readonly IRepository<ApplicationUser, Guid> applicationUserRepository;

        public VehicleTypeCategoryService(
            IRepository<VehicleTypeCategory, int> vehicleTypeCategoryRepository,
            IRepository<Vehicle, Guid> vehicleRepository,
            IRepository<ApplicationUser, Guid> applicationUserRepository)
        {
            this.vehicleTypeCategoryRepository = vehicleTypeCategoryRepository;
            this.vehicleRepository = vehicleRepository;
            this.applicationUserRepository = applicationUserRepository;
        }
        public async Task<ServiceResponse<IEnumerable<VehicleTypeCategoryViewModel>>> GetAllCategories()
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

            return ServiceResponse<IEnumerable<VehicleTypeCategoryViewModel>>.Ok(allCategories);
        }

        public async Task<ServiceResponse<bool>> DeleteByIdAsync(Guid userId, int id)
        {
            ApplicationUser currentUser = await this.applicationUserRepository
                .GetByIdAsync(userId);

            if (currentUser == null)
            {
                return ServiceResponse<bool>.Fail("User Not Found!");
            }

            if (currentUser.UserType != UserType.CompanyEmployee)
            {
                return ServiceResponse<bool>.Fail("Unauthorized User!");
            }

            VehicleTypeCategory? category = await this.vehicleTypeCategoryRepository
                .GetAllAsQueryable()
                .FirstOrDefaultAsync(vtc =>
                    vtc.Id == id &&
                    vtc.IsDeleted == false);

            if (category == null)
            {
                return ServiceResponse<bool>.Fail("Category Not Found!");
            }

            bool currentlyInUse = await this.vehicleRepository
                .GetAllAsQueryable()
                .AnyAsync(v => v.VehicleTypeCategoryId == id);

            if (currentlyInUse)
            {
                return ServiceResponse<bool>.Fail("Category Is Currently In Use!");
            }

            category.IsDeleted = true;
            await this.vehicleTypeCategoryRepository.SaveChangesAsync();

            return ServiceResponse<bool>.Ok(true);
        }

        public async Task<ServiceResponse<VehicleTypeCategoryEditFormViewModel?>> EditCategory(Guid userId, VehicleTypeCategoryEditFormViewModel viewModel)
        {
            ApplicationUser currentUser = await this.applicationUserRepository
                .GetByIdAsync(userId);

            if (currentUser == null)
            {
                return ServiceResponse<VehicleTypeCategoryEditFormViewModel?>.Fail("User Not Found!");
            }

            if (currentUser.UserType != UserType.CompanyEmployee)
            {
                return ServiceResponse<VehicleTypeCategoryEditFormViewModel?>.Fail("Unauthorized User!");
            }

            VehicleTypeCategory? category = await this.vehicleTypeCategoryRepository
                .GetAllAsQueryable()
                .FirstOrDefaultAsync(vtc =>
                    vtc.Id == viewModel.Id &&
                    vtc.IsDeleted == false);

            if (category == null)
            {
                return ServiceResponse<VehicleTypeCategoryEditFormViewModel?>.Fail("Category Not Found!");
            }

            category.Name = viewModel.Name;
            category.Description = viewModel.Description;

            await this.vehicleRepository.SaveChangesAsync();

            return ServiceResponse<VehicleTypeCategoryEditFormViewModel?>.Ok(
                new VehicleTypeCategoryEditFormViewModel()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    VehicleTypeId = category.VehicleTypeId,
                }
            );
        }

        public async Task<ServiceResponse<VehicleTypeCategoryCreateFormViewModel?>> CreateCategory(Guid userId, VehicleTypeCategoryCreateFormViewModel viewModel)
        {
            ApplicationUser currentUser = await this.applicationUserRepository
                .GetByIdAsync(userId);

            if (currentUser == null)
            {
                return ServiceResponse<VehicleTypeCategoryCreateFormViewModel?>.Fail("User Not Found!");
            }

            if (currentUser.UserType != UserType.CompanyEmployee)
            {
                return ServiceResponse<VehicleTypeCategoryCreateFormViewModel?>.Fail("Unauthorized User!");
            }

            bool alreadyExists = await this.vehicleTypeCategoryRepository
                .GetAllAsQueryable()
                .AnyAsync(vtc =>
                    vtc.Id == viewModel.Id &&
                    vtc.IsDeleted == false);

            if (alreadyExists)
            {
                return ServiceResponse<VehicleTypeCategoryCreateFormViewModel?>.Fail("Category Already Exists!");
            }

            VehicleTypeCategory newCategory = new VehicleTypeCategory()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                VehicleTypeId = viewModel.VehicleTypeId,
            };

            await this.vehicleTypeCategoryRepository.AddAsync(newCategory);
            await this.vehicleTypeCategoryRepository.SaveChangesAsync();

            return ServiceResponse<VehicleTypeCategoryCreateFormViewModel?>.Ok(
                new VehicleTypeCategoryCreateFormViewModel()
                {
                    Id = newCategory.Id,
                    Name = newCategory.Name,
                    Description = newCategory.Description,
                    VehicleTypeId = newCategory.VehicleTypeId,
                }
            );
        }
    }
}
