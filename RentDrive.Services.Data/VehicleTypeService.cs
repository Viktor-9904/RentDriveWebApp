using Microsoft.EntityFrameworkCore;

using RentDrive.Common.Enums;
using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Common;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.Enums;
using RentDrive.Web.ViewModels.VehicleType;


namespace RentDrive.Services.Data
{
    public class VehicleTypeService : IVehicleTypeService
    {
        private readonly IRepository<VehicleType, int> vehicleTypeRepository;
        private readonly IRepository<Vehicle, Guid> vehicleRepository;
        private readonly IRepository<ApplicationUser, Guid> applicationUserRepository;

        public VehicleTypeService(
            IRepository<VehicleType, int> vehicleTypeRepository,
            IRepository<Vehicle, Guid> vehicleRepository,
            IRepository<ApplicationUser, Guid> applicationUserRepository)
        {
            this.vehicleTypeRepository = vehicleTypeRepository;
            this.vehicleRepository = vehicleRepository;
            this.applicationUserRepository = applicationUserRepository;
        }

        public async Task<ServiceResponse<bool>> Exists(int vehicleTypeId)
        {
            return ServiceResponse<bool>.Ok(
                await this.vehicleTypeRepository
                .GetAllAsQueryable()
                .AnyAsync(vt =>
                    vt.Id == vehicleTypeId &&
                    vt.IsDeleted == false)
            );
        }

        public async Task<ServiceResponse<IEnumerable<VehicleTypeViewModel>>> GetAllVehicleTypesAsync()
        {
            List<VehicleTypeViewModel> vehicleTypes = await this.vehicleTypeRepository
                .GetAllAsQueryable()
                .Include(vt => vt.Vehicles)
                .Where(vt => vt.IsDeleted == false)
                .Select(vt => new VehicleTypeViewModel()
                {
                    Id = vt.Id,
                    Name = vt.Name,
                    AvailableFuels = vt.Vehicles
                        .Where(v => v.IsDeleted == false)
                        .GroupBy(v => v.FuelType)
                        .Select(g => new FuelTypeEnumViewModel()
                        {
                            Id = (int)g.Key,
                            Name = g.Key.ToString(),
                        })
                        .ToList()
                })
                .ToListAsync();

            return ServiceResponse<IEnumerable<VehicleTypeViewModel>>.Ok(vehicleTypes);
        }

        public async Task<ServiceResponse<VehicleTypeCreateFormViewModel?>> CreateNewVehicleType(Guid userId, VehicleTypeCreateFormViewModel viewModel)
        {
            ApplicationUser currentUser = await this.applicationUserRepository
                .GetByIdAsync(userId);

            if (currentUser == null)
            {
                return ServiceResponse<VehicleTypeCreateFormViewModel?>.Fail("User Not Found!");
            }

            if (currentUser.UserType != UserType.CompanyEmployee)
            {
                return ServiceResponse<VehicleTypeCreateFormViewModel?>.Fail("Unauthorized User!");
            }

            bool alreadyExists = await this.vehicleTypeRepository
                .GetAllAsQueryable()
                .AnyAsync(vt =>
                    vt.Name == viewModel.Name &&
                    vt.IsDeleted == false);

            if (alreadyExists)
            {
                return ServiceResponse<VehicleTypeCreateFormViewModel?>.Fail("Vehicle Type Already Exists!");
            }

            VehicleType newVehicleType = new VehicleType()
            {
                Name = viewModel.Name
            };

            await this.vehicleTypeRepository.AddAsync(newVehicleType);
            await this.vehicleTypeRepository.SaveChangesAsync();

            viewModel.Id = newVehicleType.Id;

            return ServiceResponse<VehicleTypeCreateFormViewModel?>.Ok(
                new VehicleTypeCreateFormViewModel
                {
                    Id = newVehicleType.Id,
                    Name = newVehicleType.Name
                }
            );
        }

        public async Task<ServiceResponse<VehicleTypeEditFormViewModel?>> EditVehicleType(Guid userId, VehicleTypeEditFormViewModel viewModel)
        {
            ApplicationUser currentUser = await this.applicationUserRepository
                .GetByIdAsync(userId);

            if (currentUser == null)
            {
                return ServiceResponse<VehicleTypeEditFormViewModel?>.Fail("User Not Found!");
            }

            if (currentUser.UserType != UserType.CompanyEmployee)
            {
                return ServiceResponse<VehicleTypeEditFormViewModel?>.Fail("Unauthorized User!");
            }

            VehicleType? vehicleType = await this.vehicleTypeRepository
                .GetAllAsQueryable()
                .FirstOrDefaultAsync(vt =>
                    vt.Id == viewModel.Id &&
                    vt.IsDeleted == false);

            if (vehicleType == null)
            {
                return ServiceResponse<VehicleTypeEditFormViewModel?>.Fail("Vehicle Type Not Found!");
            }

            vehicleType.Name = viewModel.Name;
            await this.vehicleTypeRepository.SaveChangesAsync();

            return ServiceResponse<VehicleTypeEditFormViewModel?>.Ok(
                new VehicleTypeEditFormViewModel()
                {
                    Id = vehicleType.Id,
                    Name = vehicleType.Name
                }
            );
        }

        public async Task<ServiceResponse<bool>> DeleteVehicleTypeByIdAsync(Guid userId, int id)
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

            VehicleType? vehicleType = await this.vehicleTypeRepository
                .GetAllAsQueryable()
                .FirstOrDefaultAsync(vt =>
                    vt.Id == id &&
                    vt.IsDeleted == false);

            if (vehicleType == null)
            {
                return ServiceResponse<bool>.Fail("Vehicle Type Not Found!");
            }

            bool currentlyInUse = await this.vehicleRepository
                .GetAllAsQueryable()
                .AnyAsync(v => v.VehicleTypeId == id);

            if (currentlyInUse)
            {
                return ServiceResponse<bool>.Fail("Vehicle Type Currently In Use!");
            }

            vehicleType.IsDeleted = true;
            await this.vehicleTypeRepository.SaveChangesAsync();

            return ServiceResponse<bool>.Ok(true);
        }
    }
}
