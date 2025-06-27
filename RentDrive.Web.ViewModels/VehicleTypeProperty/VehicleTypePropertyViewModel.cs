using RentDrive.Common.Enums;

namespace RentDrive.Web.ViewModels.VehicleTypeProperty
{
    public class VehicleTypePropertyViewModel
    {
        public Guid Id { get; set; }
        public int VehicleTypeId { get; set; }
        public string Name { get; set; } = null!;
        public string ValueType { get; set; } = null!;
        public string UnitOfMeasurement { get; set; } = null!;
    }
}
