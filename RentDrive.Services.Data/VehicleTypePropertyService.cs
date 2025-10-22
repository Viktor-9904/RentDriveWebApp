using Microsoft.EntityFrameworkCore;

using RentDrive.Common.Enums;
using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Common;
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

        public async Task<ServiceResponse<IEnumerable<VehicleTypePropertyViewModel>>> GetAllVehicleTypePropertiesAsync()
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

            return ServiceResponse<IEnumerable<VehicleTypePropertyViewModel>>.Ok(vehicleTypeProperties);
        }

        public async Task<ServiceResponse<VehicleTypePropertyViewModel>> CreateVehicleTypeProperty(Guid userId, CreateVehicleTypePropertyViewModel viewModel)
        {
            ServiceResponse<bool> vehicleTypeExistsResponse = await this.vehicleTypeService
                .Exists(viewModel.VehicleTypeId);

            if (!vehicleTypeExistsResponse.Success)
            {
                return ServiceResponse<VehicleTypePropertyViewModel>.Fail("Vehicle Type Not Found!");
            }

            if (!Enum.IsDefined<PropertyValueType>(viewModel.ValueType))
            {
                return ServiceResponse<VehicleTypePropertyViewModel>.Fail("Property Value Type Not Defined!");
            }

            if (!Enum.IsDefined<UnitOfMeasurement>(viewModel.UnitOfMeasurement))
            {
                return ServiceResponse<VehicleTypePropertyViewModel>.Fail("Property Unit Of Measurement Not Defined!");
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

            return ServiceResponse<VehicleTypePropertyViewModel>.Ok(
                new VehicleTypePropertyViewModel()
                {
                    Id = vehicleTypeProperty.Id,
                    VehicleTypeId = vehicleTypeProperty.VehicleTypeId,
                    Name = vehicleTypeProperty.Name,
                    ValueType = vehicleTypeProperty.ValueType.ToString(),
                    UnitOfMeasurement = vehicleTypeProperty.UnitOfMeasurement.ToString()
                }
            );
        }

        public async Task<ServiceResponse<bool>> EditPropertyAsync(Guid userId, EditVehicleTypePropertyViewModel viewModel)
        {
            if (!Enum.IsDefined<PropertyValueType>(viewModel.ValueType))
            {
                return ServiceResponse<bool>.Fail("Property Value Type Not Defined!");
            }

            if (!Enum.IsDefined<UnitOfMeasurement>(viewModel.UnitOfMeasurement))
            {
                return ServiceResponse<bool>.Fail("Property Unit Of Measurement Not Defined!");
            }

            VehicleTypeProperty? propertyEntity = await this.vehicleTypePropertyRepository
                .GetByIdAsync(viewModel.Id);

            if (propertyEntity == null)
            {
                return ServiceResponse<bool>.Fail("Property Not Found!");
            }

            propertyEntity.Name = viewModel.Name;
            propertyEntity.ValueType = viewModel.ValueType;
            propertyEntity.UnitOfMeasurement = viewModel.UnitOfMeasurement;

            bool result = await this.vehicleTypePropertyRepository.UpdateAsync(propertyEntity);

            if (!result)
            {
                return ServiceResponse<bool>.Fail("Failed To Save To Database!");
            }

            await this.vehicleTypePropertyRepository.SaveChangesAsync();
            return ServiceResponse<bool>.Ok(true);
        }

        public async Task<ServiceResponse<bool>> DeletePropertyByIdAsync(Guid userId,   Guid id)
        {
            bool result = await this.vehicleTypePropertyRepository.DeleteByIdAsync(id);

            if (!result)
            {
                return ServiceResponse<bool>.Fail("Failed To Delete From Database!");
            }

            await this.vehicleTypePropertyRepository.SaveChangesAsync();
            return ServiceResponse<bool>.Ok(true);
        }

        public async Task<ServiceResponse<bool>> ValidateVehicleTypeProperties(int VehicleTypeId, IEnumerable<VehicleTypePropertyValueInputViewModel> propertyValues)
        {
            List<VehicleTypeProperty>? expectedProperties = await this.vehicleTypePropertyRepository
                .GetAllAsQueryable()
                .Where(vtp => vtp.VehicleTypeId == VehicleTypeId)
                .ToListAsync();

            foreach (VehicleTypeProperty currentExpectedProperty in expectedProperties)
            {
                VehicleTypePropertyValueInputViewModel? submittedValue = propertyValues
                    .FirstOrDefault(vm => vm.PropertyId == currentExpectedProperty.Id);

                if (submittedValue == null)
                {
                    return ServiceResponse<bool>.Fail("Property Not Found!");
                }

                ServiceResponse<bool> isCurrentValueTypeValid = this.baseService.IsValueTypeValid(currentExpectedProperty.ValueType, submittedValue.Value);

                if (!isCurrentValueTypeValid.Success)
                {
                    return ServiceResponse<bool>.Fail("Invalid Property Format!");
                }
            }
            return ServiceResponse<bool>.Ok(true);
        }
    }
}
