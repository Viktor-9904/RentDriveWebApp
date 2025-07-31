using System.ComponentModel.DataAnnotations;
using static RentDrive.Common.EntityValidationConstants.VehicleValidationConstants.VehicleReview;

namespace RentDrive.Web.ViewModels.VehicleReview
{
    public class AddNewReviewViewModel
    {
        [Required]
        public Guid RentalId { get; set; }

        [Required]
        [Range(MinRating, MaxRating)]
        public int StarRating { get; set; }

        [Required]
        [MaxLength(CommentMaxLength)]
        public string? Comment { get; set; }
    }
}
