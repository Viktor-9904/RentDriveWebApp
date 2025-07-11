﻿using Microsoft.EntityFrameworkCore;

using RentDrive.Common.Enums;
using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.VehicleTypeProperty;

namespace RentDrive.Services.Data
{
    public class VehicleTypePropertyService : IVehicleTypePropertyService
    {
        private readonly IRepository<VehicleTypeProperty, Guid> vehicleTypePropertyRepository;
        private readonly IVehicleTypeService vehicleTypeService;

        public VehicleTypePropertyService(
            IRepository<VehicleTypeProperty, Guid> vehicleTypePropertyRepository,
            IVehicleTypeService vehicleTypeService)
        {
            this.vehicleTypePropertyRepository = vehicleTypePropertyRepository;
            this.vehicleTypeService = vehicleTypeService;
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
        public EnumOptionsViewModel GetEnumOptions()
        {
            IEnumerable<ValueTypeViewModel> valueTypes = Enum.GetValues(typeof(PropertyValueType))
                .Cast<PropertyValueType>()
                .Select(v => new ValueTypeViewModel
                {
                    Id = (int)v,
                    Name = v.ToString()
                });

            IEnumerable<UnitOfMeasurementViewModel> units = Enum.GetValues(typeof(UnitOfMeasurement))
                .Cast<UnitOfMeasurement>()
                .Select(u => new UnitOfMeasurementViewModel
                {
                    Id = (int)u,
                    Name = u.ToString()
                });

            return new EnumOptionsViewModel
            {
                ValueTypes = valueTypes,
                UnitOfMeasurements = units
            };
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
    }
}
