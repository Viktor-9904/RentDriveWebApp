using Microsoft.EntityFrameworkCore;
using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.Vehicle;

namespace RentDrive.Services.Data
{
    public class VehicleTypePropertyValueService : IVehicleTypePropertyValueService
    {
        private readonly IRepository<VehicleTypePropertyValue, Guid> vehicleTypePropertyValueRepository;

        public VehicleTypePropertyValueService(IRepository<VehicleTypePropertyValue, Guid> vehicleTypePropertyValueRepository)
        {
            this.vehicleTypePropertyValueRepository = vehicleTypePropertyValueRepository;
        }

        public async Task<List<VehicleTypePropertyValuesViewModel>> GetVehicleTypePropertyValuesByVehicleIdAsync(Guid vehicleId)
        {
            List<VehicleTypePropertyValuesViewModel> vehicleTypePropertyValues = await this.vehicleTypePropertyValueRepository
                .GetAllAsQueryable()
                .Include(vtpv => vtpv.VehicleTypeProperty)
                .Where(vtpv => vtpv.VehicleId == vehicleId)
                .Select(vtpv=> new VehicleTypePropertyValuesViewModel()
                {
                    PropertyId = vtpv.VehicleTypePropertyId,
                    VehicleTypePropertyName = vtpv.VehicleTypeProperty.Name,
                    VehicleTypePropertyValue = vtpv.PropertyValue,
                    ValueType = vtpv.VehicleTypeProperty.ValueType.ToString(),
                    UnitOfMeasurement = vtpv.VehicleTypeProperty.UnitOfMeasurement.ToString(),
                })
                .ToListAsync();

            return vehicleTypePropertyValues;
        }
    }
}