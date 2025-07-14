using Microsoft.EntityFrameworkCore;

using RentDrive.Common.Enums;
using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.VehicleTypeProperty;
using RentDrive.Web.ViewModels.VehicleTypePropertyValue;

namespace RentDrive.Services.Data
{
    public class VehicleTypePropertyService : IVehicleTypePropertyService
    {
        private readonly IRepository<VehicleTypeProperty, Guid> vehicleTypePropertyRepository;
        private readonly IVehicleTypeService vehicleTypeService;
        private readonly IBaseService baseService;

        public VehicleTypePropertyService(
            IRepository<VehicleTypeProperty, Guid> vehicleTypePropertyRepository,
            IVehicleTypeService vehicleTypeService,
            IBaseService baseService)
        {
            this.vehicleTypePropertyRepository = vehicleTypePropertyRepository;
            this.vehicleTypeService = vehicleTypeService;
            this.baseService = baseService;
        }

        public async Task<IEnumerable<VehicleTypePropertyViewModel>> GetAllVehicleTypePropertiesAsync()
        {
            IEnumerable<VehicleTypePropertyViewModel> vehicleTypeProperties = await this.vehicleTypePropertyRepository
                .GetAllAsQueryable()
                .Select(vtp => new VehicleTypePropertyViewModel()
                {
                    Id = vtp.Id,
                    VehicleTypeId = vtp.VehicleTypeId,
                    Name = vtp.Name,
                    ValueType = vtp.ValueType.ToString(),
                    UnitOfMeasurement = vtp.UnitOfMeasurement.ToString(),
                })
                .ToListAsync();

            return vehicleTypeProperties;
        }
        public async Task<bool> EditPropertyAsync(EditVehicleTypePropertyViewModel viewModel)
        {
            if (!Enum.IsDefined<PropertyValueType>(viewModel.ValueType) ||
                !Enum.IsDefined<UnitOfMeasurement>(viewModel.UnitOfMeasurement))
            {
                return false;
            }

            VehicleTypeProperty? propertyEntity = await this.vehicleTypePropertyRepository
                .GetByIdAsync(viewModel.Id);

            if (propertyEntity == null)
            {
                return false;
            }

            propertyEntity.Name = viewModel.Name;
            propertyEntity.ValueType = viewModel.ValueType;
            propertyEntity.UnitOfMeasurement = viewModel.UnitOfMeasurement;

            bool result = await this.vehicleTypePropertyRepository.UpdateAsync(propertyEntity);

            if (result)
            {
                await this.vehicleTypePropertyRepository.SaveChangesAsync();
                return true;
            }

            return false;
        }
        public async Task<bool> DeletePropertyByIdAsync(Guid id)
        {
            bool result = await this.vehicleTypePropertyRepository.DeleteByIdAsync(id);

            if (result)
            {
                await this.vehicleTypePropertyRepository.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> CreateVehicleTypeProperty(CreateVehicleTypePropertyViewModel viewModel)
        {
            bool vehicleTypeExists = await this.vehicleTypeService
                .Exists(viewModel.VehicleTypeId);

            if (!vehicleTypeExists)
            {
                return false;
            }

            if (!Enum.IsDefined<PropertyValueType>(viewModel.ValueType) ||
                !Enum.IsDefined<UnitOfMeasurement>(viewModel.UnitOfMeasurement))
            {
                return false;
            }

            VehicleTypeProperty vehicleTypeProperty = new VehicleTypeProperty()
            {
                Name = viewModel.Name,
                VehicleTypeId = viewModel.VehicleTypeId,
                ValueType = viewModel.ValueType,
                UnitOfMeasurement = viewModel.UnitOfMeasurement,
            };

            await vehicleTypePropertyRepository.AddAsync(vehicleTypeProperty);
            await vehicleTypePropertyRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ValidateVehicleTypeProperties(int VehicleTypeId, IEnumerable<VehicleTypePropertyValueInputViewModel> propertyValues)
        {
            List<VehicleTypeProperty>? expectedProperties = await this.vehicleTypePropertyRepository
                .GetAllAsQueryable()
                .Where(vtp => vtp.VehicleTypeId == VehicleTypeId)
                .ToListAsync();

            if (expectedProperties.Count == 0)
            {
                return false;
            }

            foreach (VehicleTypeProperty currentExpectedProperty in expectedProperties)
            {
                VehicleTypePropertyValueInputViewModel? submittedValue = propertyValues
                    .FirstOrDefault(vm => vm.PropertyId == currentExpectedProperty.Id);

                if (submittedValue == null)
                {
                    return false;
                }

                if (!this.baseService.IsValueTypeValid(currentExpectedProperty.ValueType, submittedValue.Value, out string? error))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
