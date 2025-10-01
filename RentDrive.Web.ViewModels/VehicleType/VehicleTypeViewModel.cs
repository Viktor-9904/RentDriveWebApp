using RentDrive.Web.ViewModels.Enums;

namespace RentDrive.Web.ViewModels.VehicleType
{
    public class VehicleTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<FuelTypeEnumViewModel> AvailableFuels { get; set; } 
            = new List<FuelTypeEnumViewModel>();
    }
}
