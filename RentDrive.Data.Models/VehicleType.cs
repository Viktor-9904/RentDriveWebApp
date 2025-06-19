namespace RentDrive.Data.Models
{
    public class VehicleType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Vehicle> Vehicles { get; set; }
            = new List<Vehicle>();
        public ICollection<VehicleTypeCategory> VehicleTypeCategory { get; set; }
            = new List<VehicleTypeCategory>();
        public ICollection<VehicleTypeProperty> VehicleTypeProperty { get; set; }
            = new List<VehicleTypeProperty>();
    }
}
