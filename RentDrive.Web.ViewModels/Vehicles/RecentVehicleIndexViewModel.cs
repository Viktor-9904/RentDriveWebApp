namespace RentDrive.Web.ViewModels.Vehicles
{
    public class RecentVehicleIndexViewModel
    {
        public Guid Id { get; set; }
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public decimal PricePerHour { get; set; }
        public string ImageURL { get; set; } = null!;
        public int YearOfProduction { get; set; }
        public string? OwnerName { get; set; }
        public string? FuelType { get; set; }
        public string? Description { get; set; }
    }
}
