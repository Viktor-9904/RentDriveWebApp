namespace RentDrive.Data.Models
{
    public class Vehicle
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? OwnerId { get; set; }
        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; } = null!;
        public int VehicleTypeCategoryId { get; set; }
        public VehicleTypeCategory VehicleTypeCategory { get; set; } = null!;
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Color { get; set; } = null!;
        public decimal PricePerHour { get; set; }
        public DateTime DateOfProduction { get; set; }
        public DateTime DateAdded { get; set; }
        public double CurbWeightInKg { get; set; }
        public int OdoKilometers { get; set; }
        public double? EngineDisplacement { get; set; }
        public FuelType? FuelType { get; set; }
        public string? Description { get; set; }
        public double? PowerInKiloWatts { get; set; }
    }
}
