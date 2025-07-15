using Microsoft.EntityFrameworkCore;

using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.VehicleType;


namespace RentDrive.Services.Data
{
    public class VehicleTypeService : IVehicleTypeService
    {
        private readonly IRepository<VehicleType, int> vehicleTypeRepository;
        private readonly IRepository<Vehicle, Guid> vehicleRepository;
        public VehicleTypeService(
            IRepository<VehicleType, int> vehicleTypeRepository,
            IRepository<Vehicle, Guid> vehicleRepository)
        {
            this.vehicleTypeRepository = vehicleTypeRepository;
            this.vehicleRepository = vehicleRepository;
        }

        public async Task<bool> Exists(int vehicleTypeId)
        {
            return await this.vehicleTypeRepository
                .GetAllAsQueryable()
                .AnyAsync(vt => 
                    vt.Id == vehicleTypeId && 
                    vt.IsDeleted == false);
        }

        public async Task<IEnumerable<VehicleTypeViewModel>> GetAllVehicleTypesAsync()
        {
            List<VehicleTypeViewModel> vehicleTypes = await this.vehicleTypeRepository
                .GetAllAsQueryable()
                .Where(vt => vt.IsDeleted == false)
                .Select(vt => new VehicleTypeViewModel()
                {
                    Id = vt.Id,
                    Name = vt.Name,
                })
                .ToListAsync();

            return vehicleTypes;
        }
        public async Task<bool> DeleteVehicleTypeByIdAsync(int id)
        {
            VehicleType? vehicleType = await this.vehicleTypeRepository
                .GetAllAsQueryable()
                .FirstOrDefaultAsync(vt => 
                    vt.Id == id &&
                    vt.IsDeleted == false);

            if (vehicleType == null)
            {
                return false;
            }

            bool currentlyInUse = await this.vehicleRepository
                .GetAllAsQueryable()
                .AnyAsync(v => v.VehicleTypeId == id);

            if (currentlyInUse)
            {
                return false;
            }

            vehicleType.IsDeleted = true;
            await this.vehicleTypeRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditVehicleType(VehicleTypeEditFormViewModel viewModel)
        {
            VehicleType? vehicleType = await this.vehicleTypeRepository
                .GetAllAsQueryable()
                .FirstOrDefaultAsync(vt => 
                    vt.Id == viewModel.Id &&
                    vt.IsDeleted == false);

            if (vehicleType == null)
            {
                return false;
            }

            vehicleType.Name = viewModel.Name;
            await this.vehicleTypeRepository.SaveChangesAsync();

            return true;
        }
    }
}
