using System.Reflection;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using RentDrive.Data.Models;
using static RentDrive.Data.Configuration.VechicleSeeder;
using static RentDrive.Data.Configuration.VehicleTypeCategorySeeder;
using static RentDrive.Data.Configuration.VehicleTypeSeeder;
using static RentDrive.Data.Configuration.VehicleImageSeeder;
using static RentDrive.Data.Configuration.VehicleTypePropertySeeder;
using static RentDrive.Data.Configuration.VehicleTypePropertyValueSeerder;

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
        public DbSet<VehicleTypeCategory> VehicleTypeCategories { get; set; } = null!;
        public DbSet<VehicleImage> VehicleImages { get; set; } = null!;
        public DbSet<VehicleTypeProperty> VehicleTypeProperties { get; set; } = null!;
        public DbSet<VehicleTypePropertyValue> VehicleTypePropertyValues { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<VehicleType>().HasData(SeedVehicleTypes());
            modelBuilder.Entity<VehicleTypeCategory>().HasData(SeedVehicleTypeCategories());
            modelBuilder.Entity<Vehicle>().HasData(SeedVehicles());
            modelBuilder.Entity<VehicleImage>().HasData(SeedVehicleImages());
            modelBuilder.Entity<VehicleTypeProperty>().HasData(SeedVehicleTypeProperties());
            modelBuilder.Entity<    VehicleTypePropertyValue>().HasData(SeedVehicleTypePropertyValues());
        }
    }
}
