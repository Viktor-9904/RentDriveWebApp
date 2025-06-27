namespace RentDrive.Web.ViewModels.Vehicle
{
    public class ListingVehicleViewModel
    {
        public Guid Id { get; set; }
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string VehicleType { get; set; } = null!;
        public string VehicleTypeCategory { get ; set; } = null!;
        public int YearOfProduction { get; set; }
        public decimal PricePerDay { get; set; }
        public string? FuelType { get; set; }
        public string ImageURL { get; set; } = null!;
        public string? OwnerName { get; set; }
    }
}
