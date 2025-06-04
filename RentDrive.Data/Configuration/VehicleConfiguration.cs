using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RentDrive.Data.Models;
using static RentDrive.Common.EntityValidationConstants.Vehicle;

namespace RentDrive.Data.Configuration
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder
                .HasKey(v => v.Id);

            builder
                .Property(v => v.OwnerId)
                .HasComment("The owner's Id of the vehicle. Null if the vehicle is company-owned.");

            builder
                .HasOne(v => v.VehicleType)
                .WithMany(vt => vt.Vehicles)
                .HasForeignKey(v => v.VehicleTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(v => v.Owner)
                .WithMany(au => au.Vehicles)
                .HasForeignKey(v => v.OwnerId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(v => v.VehicleTypeCategory)
                .WithMany(vt => vt.Vehicles)
                .HasForeignKey(v => v.VehicleTypeCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(v => v.Make)
                .HasMaxLength(MakeMaxLength)
                .IsRequired()
                .HasComment("Manufacturer of the vehicle.");

            builder
                .Property(v => v.Model)
                .HasMaxLength(ModelMaxLength)
                .IsRequired()
                .HasComment("Model of the vehicle.");

            builder
                .Property(v => v.Color)
                .HasMaxLength(ColorMaxLength)
                .IsRequired()
                .HasComment("Vehicle's color.");

            builder
                .Property(v => v.PricePerHour)
                .HasColumnType("decimal(18,2)")
                .HasComment("Price per hour for renting the vehicle.");

            builder
                .Property(v => v.DateOfProduction)
                .IsRequired()
                .HasComment("Vehicle's manufactured date.");

            builder
                .Property(v => v.CurbWeightInKg)
                .IsRequired()
                .HasComment("Weight of the vehicle when empty in kilograms.");

            builder
                .Property(v => v.OdoKilometers)
                .IsRequired()
                .HasComment("Vehicle's total kilometers traveled.");

            builder
                .Property(v => v.EngineDisplacement)
                .IsRequired()
                .HasComment("Vehicle's engine capacity in liters.");

            builder
                .Property(v => v.FuelType)
                .IsRequired()
                .HasComment("Vehicle's fuel type.");

            builder
                .Property(v => v.Description)
                .HasMaxLength(DescriptionMaxLength)
                .HasComment("Optional description of the vehicle.");

            builder
                .Property(v => v.PowerInKiloWatts)
                .IsRequired()
                .HasComment("Power output of the vehicle's engine in kilowatts.");
        }
    }
    public static class VechicleSeeder
    {
        public static IEnumerable<Vehicle> SeedVehicles()
        {
            IEnumerable<Vehicle> vehicles = new List<Vehicle>()
            {
                new()
                {
                    Id = Guid.Parse("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"),
                    OwnerId = null,
                    VehicleTypeId = 1,
                    VehicleTypeCategoryId = 2,
                    Make = "Toyota",
                    Model = "Camry",
                    Color = "White",
                    PricePerHour = 12.50m,
                    DateOfProduction = new DateTime(2021, 5, 10),
                    DateAdded = new DateTime(2022, 7, 12),
                    CurbWeightInKg = 1470,
                    OdoKilometers = 34500,
                    EngineDisplacement = 2.5,
                    FuelType = FuelType.Petrol,
                    Description = "Comfortable midsize sedan, ideal for long drives.",
                    PowerInKiloWatts = 150
                },
                new()
                {
                    Id = Guid.Parse("fe15cde2-1a90-46d4-89f1-10fda7f11743"),
                    OwnerId = null,
                    VehicleTypeId = 1,
                    VehicleTypeCategoryId = 1,
                    Make = "Jeep",
                    Model = "Grand Cherokee",
                    Color = "Dark Green",
                    PricePerHour = 22.00m,
                    DateOfProduction = new DateTime(2022, 7, 20),
                    DateAdded = new DateTime(2023, 2, 22),
                    CurbWeightInKg = 2045,
                    OdoKilometers = 27500,
                    EngineDisplacement = 3.6,
                    FuelType = FuelType.Petrol,
                    Description = "Spacious and off-road capable SUV.",
                    PowerInKiloWatts = 213
                },
                new()
                {
                    Id = Guid.Parse("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"),
                    OwnerId = null,
                    VehicleTypeId = 1,
                    VehicleTypeCategoryId = 3,
                    Make = "Volkswagen",
                    Model = "Golf",
                    Color = "Silver",
                    PricePerHour = 10.00m,
                    DateOfProduction = new DateTime(2021, 3, 11),
                    DateAdded = new DateTime(2024, 9, 19),
                    CurbWeightInKg = 1300,
                    OdoKilometers = 19800,
                    EngineDisplacement = 1.4,
                    FuelType = FuelType.Petrol,
                    Description = "Compact hatchback with great fuel economy.",
                    PowerInKiloWatts = 103
                }
            };
            return vehicles;
        }
    }
}