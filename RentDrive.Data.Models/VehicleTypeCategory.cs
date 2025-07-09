namespace RentDrive.Data.Models
{
    public class VehicleTypeCategory
    {
        public int Id { get; set; }
        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; } = null!;
        public string Name { get; set; } = null!; // e.g. : "SUV", "Sedan", "Pickup" ...
        public string Description { get; set; } = null!;
        public ICollection<Vehicle> Vehicles { get; set; }
            = new List<Vehicle>();
    }
}
