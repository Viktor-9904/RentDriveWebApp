namespace RentDrive.Data.Models
{
    public class VehicleType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Vehicle> Vehicles { get; set; }
            = new HashSet<Vehicle>();
        public ICollection<VehicleTypeCategory> VehicleTypeCategory { get; set; }
            = new HashSet<VehicleTypeCategory>();
    }
}
