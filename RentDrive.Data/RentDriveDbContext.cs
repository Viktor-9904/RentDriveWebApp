using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentDrive.Data.Models;

namespace RentDrive.Data
{
    public class RentDriveDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        protected RentDriveDbContext()
        {

        }
        public RentDriveDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
