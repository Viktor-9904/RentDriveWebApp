namespace RentDrive.Data.Models
{
    public class VehicleTypePropertyValue
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = null!;
        public Guid VehicleTypePropertyId { get; set; }
        public VehicleTypeProperty VehicleTypeProperty { get; set; } = null!;
        public string PropertyValue { get; set; } = null!;
    }
}
