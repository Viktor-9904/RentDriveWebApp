using RentDrive.Common.Enums;
using RentDrive.Web.ViewModels.Enums;

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
    }
}
