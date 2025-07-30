using System.ComponentModel.DataAnnotations;

using static RentDrive.Common.EntityValidationConstants.VehicleValidationConstants.VehicleReview;

namespace RentDrive.Data.Models
{
    public class VehicleReview
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = null!;
        public Guid RentalId { get; set; }
        public Rental Rental { get; set; } = null!;
        public Guid ReviewerId { get; set; }
        public ApplicationUser Reviewer { get; set; } = null!;
        public int Stars { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
