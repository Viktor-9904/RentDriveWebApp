using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RentDrive.Data.Models;
using static RentDrive.Common.EntityValidationConstants.VehicleType;

namespace RentDrive.Data.Configuration
{
    public class VehicleTypeConfiguration : IEntityTypeConfiguration<VehicleType>
    {
        public void Configure(EntityTypeBuilder<VehicleType> builder)
        {
            builder
                .HasKey(vt => vt.Id);

            builder
                .Property(vt => vt.Name)
                .HasMaxLength(NameMaxLength)
                .IsRequired()
                .HasComment("Name of the vehicle type.");
        }
    }

    public static class VehicleTypeSeeder
    {
        public static IEnumerable<VehicleType> SeedVehicleTypes()
        {
            IEnumerable<VehicleType> vehicleTypes = new List<VehicleType>()
            {
                new()
                {
                    Id = 1,
                    Name = "Car"
                },
                new()
                {
                    Id = 2,
                    Name = "Truck"
                },
                new()
                {
                    Id = 3,
                    Name = "Motorcycle"
                },
                new()
                {
                    Id = 4,
                    Name = "Bicycle"
                },
                new()
                {
                    Id = 5,
                    Name = "Electric Scooter"
                },
                new()
                {
                    Id = 6,
                    Name = "All Terrain Vehicle (ATV)"
                },
                new()
                {
                    Id = 7,
                    Name = "Camper Trailer"
                },
                new()
                {
                    Id = 8,
                    Name = "Recreational Vehicle"
                },
                new()
                {
                    Id = 9,
                    Name = "Limousine"
                }
            };
            return vehicleTypes;
        }
    }
}