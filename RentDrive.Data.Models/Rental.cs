using Microsoft.VisualBasic;
using RentDrive.Common.Enums;
using System.Net.Http.Headers;

namespace RentDrive.Data.Models
{
    public class Rental
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = null!;
        public Guid RenterId { get; set; }
        public ApplicationUser Renter { get; set; } = null!;
        public DateTime BookedOn { get; set; } = DateTime.UtcNow;
        public DateTime? CancelledOn { get; set; }
        public DateTime? CompletedOn { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public RentalStatus Status { get; set; } = RentalStatus.Active;
    }
}
