using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RentDrive.Common.Enums;
using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.Vehicle;
using RentDrive.Web.ViewModels.VehicleTypeProperty;
using RentDrive.Web.ViewModels.VehicleTypePropertyValue;
using System.Transactions;

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
                .Select(vtpv => new VehicleTypePropertyValuesViewModel()
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

        public async Task<bool> AddVehicleTypePropertyValuesAsync(Guid vehicleId, IEnumerable<VehicleTypePropertyValueInputViewModel> submittedPropertyValues)
        {
            List<VehicleTypePropertyValue> propertyValues = new List<VehicleTypePropertyValue>();

            foreach (VehicleTypePropertyValueInputViewModel currentProperty in submittedPropertyValues)
            {
                VehicleTypePropertyValue value = new VehicleTypePropertyValue()
                {
                    VehicleId = vehicleId,
                    VehicleTypePropertyId = currentProperty.PropertyId,
                    PropertyValue = currentProperty.Value
                };

                propertyValues.Add(value);
            }

            await vehicleTypePropertyValueRepository.AddRangeAsync(propertyValues.ToArray());

            return true;
        }

        public async Task<bool> UpdateVehicleTypePropertyValuesAsync(Guid vehicleId, IEnumerable<VehicleTypePropertyValueInputViewModel> submittedPropertyValues)
        {
            foreach (VehicleTypePropertyValueInputViewModel currentProperty in submittedPropertyValues)
            {
                VehicleTypePropertyValue? propertyValueToUpdate = await this.vehicleTypePropertyValueRepository
                    .GetAllAsQueryable()
                    .FirstOrDefaultAsync(vtpv =>
                        vtpv.VehicleTypePropertyId == currentProperty.PropertyId &&
                        vtpv.VehicleId == vehicleId);

                if (propertyValueToUpdate == null)
                {
                    return false;
                }

                propertyValueToUpdate.PropertyValue = currentProperty.Value;
            }

            await this.vehicleTypePropertyValueRepository.SaveChangesAsync();
            return true;
        }
    }
}