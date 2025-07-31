using RentDrive.Web.ViewModels.VehicleReview;

namespace RentDrive.Web.ViewModels.Vehicle
{
    public class VehicleDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string? OwnerName { get; set; }
        public int VehicleTypeId { get; set; }
        public string VehicleType { get; set; } = null!;
        public int VehicleTypeCategoryId { get; set; }
        public string VehicleTypeCategory { get; set; } = null!;
        public string Color { get; set; } = null!;
        public decimal PricePerDay { get; set; }
        public DateTime DateOfProduction { get; set; }
        public DateTime DateAdded { get; set; }
        public double CurbWeightInKg { get; set; }
        public string FuelType { get; set; } = null!;
        public string? Description { get; set; }
        public double StarsRating { get; set; }
        public int ReviewCount { get; set; }
        public List<string> ImageURLS { get; set; }
            = new List<string>();
        public List<VehicleTypePropertyValuesViewModel> VehicleProperties { get; set; }
            = new List<VehicleTypePropertyValuesViewModel>();
        public List<VehicleReviewListItemViewModel> VehicleReviews { get; set; }
            = new List<VehicleReviewListItemViewModel>();
    }
}
