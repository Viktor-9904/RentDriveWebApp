using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentDrive.Data.Models;
using static RentDrive.Common.Vehicle.VehicleValidationConstants.VehicleType;

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

            builder
                .Property(vt => vt.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false)
                .HasComment("Is vehicle type soft deleted");
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
                    Name = "Car",
                    IsDeleted = false,

                },
                new()
                {
                    Id = 2,
                    Name = "Truck",
                    IsDeleted = false,

                },
                new()
                {
                    Id = 3,
                    Name = "Motorcycle",
                    IsDeleted = false,

                },
                new()
                {
                    Id = 4,
                    Name = "Bicycle",
                    IsDeleted = false,

                },
                new()
                {
                    Id = 5,
                    Name = "Electric Scooter",
                    IsDeleted = false,

                },
                new()
                {
                    Id = 6,
                    Name = "All Terrain Vehicle (ATV)",
                    IsDeleted = false,

                },
                new()
                {
                    Id = 7,
                    Name = "Camper Trailer",
                    IsDeleted = false,

                },
                new()
                {
                    Id = 8,
                    Name = "Recreational Vehicle",
                    IsDeleted = false,

                },
                new()
                {
                    Id = 9,
                    Name = "Limousine",
                    IsDeleted = false,

                }
            };
            return vehicleTypes;
        }
    }
}