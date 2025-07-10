using RentDrive.Common.Enums;
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
        public bool IsValueTypeValid(PropertyValueType valueType, string value, out string error)
        {
            error = null;

            switch (valueType)
            {
                case PropertyValueType.Int:
                    if (!int.TryParse(value, out _))
                    {
                        error = "Must be an integer.";
                        return false;
                    }
                    break;
                case PropertyValueType.Double:
                    if (!double.TryParse(value, out _))
                    {
                        error = "Must be a double number.";
                        return false;
                    }
                    break;
                case PropertyValueType.Boolean:
                    if (!bool.TryParse(value, out _))
                    {
                        error = "Must be true or false.";
                        return false;
                    }
                    break;
            }

            return true;
        }

    }
}
