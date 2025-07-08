using RentDrive.Web.ViewModels.Enums;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IBaseService
    {
        public IEnumerable<FuelTypeEnumViewModel> GetFuelTypesEnum();
    }
}
