using Microsoft.AspNetCore.Identity;

namespace RentDrive.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            this.CreatedOn = DateTime.UtcNow;
        }
        public DateTime CreatedOn { get; set; }
        public bool IsCompanyEmployee { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
            = new HashSet<Vehicle>();
        public ICollection<Rental> Rentals { get; set; } 
            = new List<Rental>();
        public ICollection<VehicleReview> ReviewsGiven { get; set; }
             = new List<VehicleReview>();
    }
}
