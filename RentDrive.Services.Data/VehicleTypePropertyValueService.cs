using Microsoft.EntityFrameworkCore;

using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Common;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.Vehicle;
using RentDrive.Web.ViewModels.VehicleTypePropertyValue;

namespace RentDrive.Services.Data
{
    public class VehicleTypePropertyValueService : IVehicleTypePropertyValueService
    {
        private readonly IRepository<VehicleTypePropertyValue, Guid> vehicleTypePropertyValueRepository;
        private readonly IRepository<VehicleTypeProperty, Guid> vehicleTypePropertyRepository;
        private readonly IRepository<VehicleType, int> vehicleTypeRepository;

        public VehicleTypePropertyValueService(
            IRepository<VehicleTypePropertyValue, Guid> vehicleTypePropertyValueRepository,
            IRepository<VehicleTypeProperty, Guid> vehicleTypePropertyRepository,
            IRepository<VehicleType, int> vehicleTypeRepository)
        {
            this.vehicleTypePropertyValueRepository = vehicleTypePropertyValueRepository;
            this.vehicleTypePropertyRepository = vehicleTypePropertyRepository;
            this.vehicleTypeRepository = vehicleTypeRepository;
        }

        public async Task<ServiceResponse<List<VehicleTypePropertyValuesViewModel>>> GetVehicleTypePropertyValuesByVehicleIdAsync(Guid vehicleId)
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

            return ServiceResponse<List<VehicleTypePropertyValuesViewModel>>.Ok(vehicleTypePropertyValues);
        }

        public async Task<ServiceResponse<bool>> AddVehicleTypePropertyValuesAsync(Guid vehicleId, IEnumerable<VehicleTypePropertyValueInputViewModel> submittedPropertyValues)
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

            return ServiceResponse<bool>.Ok(true);
        }

        public async Task<ServiceResponse<bool>> UpdateVehicleTypePropertyValuesAsync(Guid vehicleId, IEnumerable<VehicleTypePropertyValueInputViewModel> submittedPropertyValues)
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
                    return ServiceResponse<bool>.Fail("Property Value Not Found!");
                }

                propertyValueToUpdate.PropertyValue = currentProperty.Value;
            }

            await this.vehicleTypePropertyValueRepository.SaveChangesAsync();
            return ServiceResponse<bool>.Ok(true);
        }

        public async Task<ServiceResponse<FilterTypeProperties?>> LoadTypePropertyValuesByTypeIdAsync(int vehicleTypeId)
        {
            VehicleType? vehicleType = await this.vehicleTypeRepository
                .GetAllAsQueryable()
                .Where(vt => vt.Id == vehicleTypeId && !vt.IsDeleted)
                .FirstOrDefaultAsync();

            if (vehicleType == null)
            {
                return ServiceResponse<FilterTypeProperties?>.Fail("Vehicle Type Not Found!");
            }

            List<VehicleTypeProperty> properties = await this.vehicleTypePropertyRepository
                .GetAllAsQueryable()
                .Where(p => p.VehicleTypeId == vehicleTypeId)
                .ToListAsync();

            if (properties == null)
            {
                return ServiceResponse<FilterTypeProperties?>.Fail("Vehicle Type Properties Not Found!");
            }

            List<FilterProperty> filterProperties = new List<FilterProperty>();

            foreach (VehicleTypeProperty property in properties)
            {
                List<FilterPropertyValue> valuesWithCounts = await vehicleTypePropertyValueRepository
                    .GetAllAsQueryable()
                    .Include(vtpv => vtpv.Vehicle)
                    .Where(vtpv => vtpv.VehicleTypePropertyId == property.Id && vtpv.Vehicle.IsDeleted == false)
                    .GroupBy(vtpv => vtpv.PropertyValue)
                    .Select(g => new FilterPropertyValue
                    {
                        PropertyValue = g.Key,
                        UnitOfMeasurement = property.UnitOfMeasurement.ToString(),
                        Count = g.Count()
                    })
                    .ToListAsync();

                filterProperties.Add(new FilterProperty
                {
                    PropertyId = property.Id,
                    PropertyName = property.Name,
                    PropertyValues = valuesWithCounts
                });
            }

            return ServiceResponse<FilterTypeProperties?>.Ok(
                new FilterTypeProperties
                {
                    TypeId = vehicleType.Id,
                    Name = vehicleType.Name,
                    Properties = filterProperties
                }
            );
        }
    }
}