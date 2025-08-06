namespace RentDrive.Web.ViewModels.Vehicle
{
    public class UserVehicleViewModel
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string FuelType { get; set; } = null!;
        public decimal PricePerDay { get; set; }
        public int TimesBooked { get; set; }
        public double Rating { get; set; }
        public double StarRating { get; set; }
        public int ReviewCount { get; set; }
    }
}