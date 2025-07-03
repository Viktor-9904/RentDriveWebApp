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
        public VehicleTypeService(IRepository<VehicleType, int> vehicleTypeRepository)
        {
            this.vehicleTypeRepository = vehicleTypeRepository;
        }

        public async Task<bool> Exists(int vehicleTypeId)
        {
            return await this.vehicleTypeRepository
                .GetAllAsQueryable()
                .AnyAsync(vt => vt.Id == vehicleTypeId);
        }

        public async Task<IEnumerable<VehicleTypeViewModel>> GetAllVehicleTypesAsync()
        {
            List<VehicleTypeViewModel> vehicleTypes = await this.vehicleTypeRepository
                .GetAllAsQueryable()
                .Select(vt => new VehicleTypeViewModel()
                {
                    Id = vt.Id,
                    Name = vt.Name,
                })
                .ToListAsync();

            return vehicleTypes;
        }
    }
}
