using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentDrive.Common.Enums;
using RentDrive.Data.Models;
using static RentDrive.Common.EntityValidationConstants.VehicleValidationConstants.Vehicle;

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
                .Property(v => v.IsDeleted)
                .IsRequired()
                .HasComment("Indicates whether the vehicle is soft deleted.")
                .HasDefaultValue(false);

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
                .Property(v => v.PricePerDay)
                .HasColumnType("decimal(18,2)")
                .HasComment("Price per day for renting the vehicle.");

            builder
                .Property(v => v.DateOfProduction)
                .IsRequired()
                .HasComment("Vehicle's manufactured date.");

            builder
                .Property(v => v.CurbWeightInKg)
                .IsRequired()
                .HasComment("Weight of the vehicle when empty in kilograms.");

            builder
                .Property(v => v.Description)
                .HasMaxLength(DescriptionMaxLength)
                .HasComment("Optional description of the vehicle.");

            builder
                .Property(v => v.FuelType)
                .IsRequired()
                .HasComment("Vehicle fuel type.")
                .HasDefaultValue(FuelType.None);
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
                    IsDeleted = false,
                    OwnerId = null,
                    VehicleTypeId = 1,
                    VehicleTypeCategoryId = 2,
                    Make = "Toyota",
                    Model = "Camry",
                    Color = "White",
                    PricePerDay = 32.50m,
                    DateOfProduction = new DateTime(2021, 5, 10),
                    DateAdded = new DateTime(2022, 7, 12),
                    CurbWeightInKg = 1470,
                    Description = "Comfortable midsize sedan, ideal for long drives.",
                    FuelType = FuelType.Petrol,
                },
                new()
                {
                    Id = Guid.Parse("fe15cde2-1a90-46d4-89f1-10fda7f11743"),
                    IsDeleted = false,
                    OwnerId = null,
                    VehicleTypeId = 1,
                    VehicleTypeCategoryId = 1,
                    Make = "Jeep",
                    Model = "Grand Cherokee",
                    Color = "Dark Green",
                    PricePerDay = 62.00m,
                    DateOfProduction = new DateTime(2022, 7, 20),
                    DateAdded = new DateTime(2023, 2, 22),
                    CurbWeightInKg = 2045,
                    Description = "Spacious and off-road capable SUV.",
                    FuelType= FuelType.Diesel,
                },
                new()
                {
                    Id = Guid.Parse("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"),
                    IsDeleted = false,
                    OwnerId = null,
                    VehicleTypeId = 1,
                    VehicleTypeCategoryId = 3,
                    Make = "Volkswagen",
                    Model = "Golf",
                    Color = "Silver",
                    PricePerDay = 54.00m,
                    DateOfProduction = new DateTime(2021, 3, 11),
                    DateAdded = new DateTime(2024, 9, 19),
                    CurbWeightInKg = 1300,
                    Description = "Compact hatchback with great fuel economy.",
                    FuelType = FuelType.Petrol
                }
            };
            return vehicles;
        }
    }
}