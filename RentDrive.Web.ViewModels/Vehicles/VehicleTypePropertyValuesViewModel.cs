namespace RentDrive.Web.ViewModels.Vehicles
{
    public class VehicleTypePropertyValuesViewModel
    {
        public Guid PropertyId { get; set; }
        public string VehicleTypePropertyName { get; set; } = null!;
        public string VehicleTypePropertyValue { get; set; } = null!;
        public string ValueType { get; set; } = null!;
        public string UnitOfMeasurement { get; set; } = null!;
    }
}
