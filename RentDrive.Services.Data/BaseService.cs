using RentDrive.Common.Enums;
using RentDrive.Services.Data.Common;
using RentDrive.Web.ViewModels.Enums;
using RentDrive.Web.ViewModels.VehicleTypeProperty;

namespace RentDrive.Services.Data.Interfaces
{
    public class BaseService : IBaseService
    {
        public IEnumerable<FuelTypeEnumViewModel> GetFuelTypesEnum()
        {
            IEnumerable<FuelTypeEnumViewModel> fuelTypes = Enum.GetValues(typeof(FuelType))
                .Cast<FuelType>()
                .Select(ft => new FuelTypeEnumViewModel
                {
                    Id = (int)ft,
                    Name = ft.ToString()
                });

            return fuelTypes;
        }
        public IEnumerable<ValueTypeViewModel> GetValueTypesEnum()
        {
            IEnumerable<ValueTypeViewModel> valueTypes = Enum.GetValues(typeof(PropertyValueType))
                .Cast<PropertyValueType>()
                .Select(v => new ValueTypeViewModel
                {
                    Id = (int)v,
                    Name = v.ToString()
                });

            return valueTypes;
        }
        public IEnumerable<UnitOfMeasurementViewModel> GetUnitsEnum()
        {
            IEnumerable<UnitOfMeasurementViewModel> units = Enum.GetValues(typeof(UnitOfMeasurement))
                .Cast<UnitOfMeasurement>()
                .Select(u => new UnitOfMeasurementViewModel
                {
                    Id = (int)u,
                    Name = u.ToString()
                });

            return units;
        }
        public ServiceResponse<bool> IsValueTypeValid(PropertyValueType valueType, string value)
        {
            switch (valueType)
            {
                case PropertyValueType.Int:
                    if (!int.TryParse(value, out _))
                    {
                        return ServiceResponse<bool>.Fail("Value Must Be An Integer!");
                    }
                    break;
                case PropertyValueType.Double:
                    if (!double.TryParse(value, out _))
                    {
                        return ServiceResponse<bool>.Fail("Value Must Be An Double!");
                    }
                    break;
                case PropertyValueType.Boolean:
                    if (!bool.TryParse(value, out _))
                    {
                        return ServiceResponse<bool>.Fail("Value Must Be A Boolean!");
                    }
                    break;
            }

            return ServiceResponse<bool>.Ok(true);
        }
    }
}
