using RentDrive.Common.Enums;
using RentDrive.Services.Data.Common;
using RentDrive.Web.ViewModels.Enums;
using RentDrive.Web.ViewModels.VehicleTypeProperty;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IBaseService
    {
        public IEnumerable<FuelTypeEnumViewModel> GetFuelTypesEnum();
        public IEnumerable<ValueTypeViewModel> GetValueTypesEnum();
        public IEnumerable<UnitOfMeasurementViewModel> GetUnitsEnum();
        public ServiceResponse<bool> IsValueTypeValid(PropertyValueType valueType, string value);
    }
}
