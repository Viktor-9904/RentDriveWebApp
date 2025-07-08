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
    }
}
