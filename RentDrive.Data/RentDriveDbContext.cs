using System.Reflection;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using RentDrive.Data.Models;
using static RentDrive.Data.Configuration.VechicleSeeder;
using static RentDrive.Data.Configuration.VehicleTypeCategorySeeder;
using static RentDrive.Data.Configuration.VehicleTypeSeeder;

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

        public DbSet<Vehicle> Vehicles { get; set; } = null!;
        public DbSet<VehicleType> VehicleTypes { get; set; } = null!;
        public DbSet<VehicleTypeCategory> VehicleTypeClasses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<VehicleType>().HasData(SeedVehicleTypes());
            modelBuilder.Entity<VehicleTypeCategory>().HasData(SeedVehicleTypeCategories());
            modelBuilder.Entity<Vehicle>().HasData(SeedVehicles());

        }
    }
}
