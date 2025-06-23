using Microsoft.EntityFrameworkCore;
using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Interfaces;

namespace RentDrive.Services.Data
{
    public class VehicleTypePropertyValueService : IVehicleTypePropertyValueService
    {
        private readonly IRepository<VehicleTypePropertyValue, Guid> vehicleTypePropertyValueRepository;

        public VehicleTypePropertyValueService(IRepository<VehicleTypePropertyValue, Guid> vehicleTypePropertyValueRepository)
        {
            this.vehicleTypePropertyValueRepository = vehicleTypePropertyValueRepository;
        }

        public async Task<IEnumerable<VehicleTypePropertyValue>> GetVehicleTypePropertyValuesByVehicleIdAsync(Guid vehicleId)
        {
            List<VehicleTypePropertyValue> vehicleTypePropertyValues = await this.vehicleTypePropertyValueRepository
                .GetAllAsQueryable()
                .Where(vtpv => vtpv.VehicleId == vehicleId)
                .ToListAsync();

            return vehicleTypePropertyValues;
        }
    }
}