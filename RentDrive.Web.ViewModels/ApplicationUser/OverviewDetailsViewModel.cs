using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentDrive.Web.ViewModels.ApplicationUser
{
    public class OverviewDetailsViewModel
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; } 
        public DateTime MemberSince { get; set; }
        public int CompletedRentalsCount { get; set; }
        public double UserRating { get; set; }
        public int VehiclesListedCount { get; set; }
    }
}
