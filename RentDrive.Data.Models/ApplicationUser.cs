using Microsoft.AspNetCore.Identity;
using RentDrive.Common.Enums;

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
        public UserType UserType { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
            = new HashSet<Vehicle>();
        public ICollection<Rental> Rentals { get; set; } 
            = new List<Rental>();
        public ICollection<VehicleReview> ReviewsGiven { get; set; }
             = new List<VehicleReview>();
        public Wallet Wallet { get; set; } = null!;
        public ICollection<ChatMessage> SentMessages { get; set; }
            = new List<ChatMessage>();
        public ICollection<ChatMessage> ReceivedMessages { get; set; }
            = new List<ChatMessage>();

    }
}
