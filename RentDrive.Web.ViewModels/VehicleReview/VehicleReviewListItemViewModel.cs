namespace RentDrive.Web.ViewModels.VehicleReview
{
    public class VehicleReviewListItemViewModel
    {
        public string Username { get; set; } = null!;
        public string? Comment { get; set; } = null!;
        public int StarRating { get; set; }
    }
}
