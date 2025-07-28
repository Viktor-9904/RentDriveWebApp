using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentDrive.Web.ViewModels.Rental
{
    public class UserVehicleRentalViewModel // User's vehicles
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string BookedOn { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Period { get; set; } = null!;
        public string Status { get; set; } = null!;
        public decimal PricePerDay { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
