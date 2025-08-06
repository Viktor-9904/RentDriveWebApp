namespace RentDrive.Web.ViewModels.Rental
{
    public class UserRentalViewModel
    {
        public Guid Id { get; set; }
        public string VehicleMake { get; set; } = null!;
        public string VehicleModel { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime BookedOn { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
        public decimal PricePerDay { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsConfirmable { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsCancellable { get; set; }
        public bool HasReviewedVehicle { get; set; }
    }
}
