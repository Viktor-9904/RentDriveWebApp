using RentDrive.Data.Models.Enums;

namespace RentDrive.Data.Models
{
    public class VehicleTypeProperty
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; } = null!;
        public string Name { get; set; } = null!;
        public AttributeValueType ValueType { get; set; }
        public UnitOfMeasurement UnitOfMeasurement { get; set; } = UnitOfMeasurement.None;
    }
}
