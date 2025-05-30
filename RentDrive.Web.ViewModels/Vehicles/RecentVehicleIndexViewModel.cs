namespace RentDrive.Web.ViewModels.Vehicles
{
    public class RecentVehicleIndexViewModel
    {
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public decimal PricePerHour { get; set; }
        public string FuelType { get; set; } = null!;
        public string? Description { get; set; }
    }
}
