namespace RentDrive.Web.ViewModels.Vehicle
{
    public class RecentVehicleIndexViewModel
    {
        public Guid Id { get; set; }
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string VehicleType { get; set; } = null!;
        public string VehicleTypeCategory { get; set; } = null!;
        public decimal PricePerDay { get; set; }
        public string ImageURL { get; set; } = null!;
        public int YearOfProduction { get; set; }
        public Guid? OwnerId { get; set; }
        public string? OwnerName { get; set; }
        public string? FuelType { get; set; }
        public string? Description { get; set; }
        public double StarsRating { get; set; }
        public int ReviewCount { get; set; }
    }
}
