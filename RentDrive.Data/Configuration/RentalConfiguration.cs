using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentDrive.Common.Enums;
using RentDrive.Data.Models;

namespace RentDrive.Data.Configuration
{
    public class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder
                .HasKey(r => r.Id);

            builder
                .HasOne(r => r.Vehicle)
                .WithMany(v => v.Rentals)
                .HasForeignKey(r => r.VehicleId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(r => r.Renter)
                .WithMany(au => au.Rentals)
                .HasForeignKey(r => r.RenterId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(r => r.BookedOn)
                .IsRequired()
                .HasComment("Date of booking.");

            builder
                .Property(r => r.CancelledOn)
                .HasComment("Date of cancelled rental.");

            builder
                .Property(r => r.CompletedOn)
                .HasComment("Rental date of completion.");

            builder
                .Property(r => r.StartDate)
                .IsRequired()
                .HasComment("Start day of rental.");

            builder
                .Property(r => r.EndDate)
                .IsRequired()
                .HasComment("End day of rental.");

            builder
                .Property(r => r.VehiclePricePerDay)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasComment("Vehicle price per day.");

            builder
                .Property(r => r.TotalPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasComment("Total price for renting.");

            builder
                .Property(r => r.Status)
                .IsRequired()
                .HasComment("Status of rental")
                .HasDefaultValue(RentalStatus.Active);
        }
    }
    public static class RentalSeeder
    {
        public static IEnumerable<Rental> SeedRentals()
        {
            IEnumerable<Rental> rentals = new List<Rental>()
        {
            // Toyota Camry
            new()
            {
                Id = Guid.Parse("a1a25e78-2b30-4f77-a899-08db1682a00a"),
                VehicleId = Guid.Parse("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"),
                RenterId = Guid.Parse("d56b4e71-7c38-4c3f-8c85-ff2b7cfd2f01"),
                BookedOn = new DateTime(2024, 12, 15),
                StartDate = new DateTime(2025, 1, 2),
                EndDate = new DateTime(2025, 1, 6),
                VehiclePricePerDay = 32.50m,
                TotalPrice = 130.00m,
                Status = RentalStatus.Completed,
                CompletedOn = new DateTime(2025, 1, 6)
            },
            new()
            {
                Id = Guid.Parse("9cfe3db6-50e1-41e4-8a98-3cdba63c20b1"),
                VehicleId = Guid.Parse("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"),
                RenterId = Guid.Parse("e7df3bc2-1c20-4895-b8c9-781ad6cf892a"),
                BookedOn = new DateTime(2025, 6, 1),
                StartDate = new DateTime(2025, 7, 10),
                EndDate = new DateTime(2025, 7, 15),
                VehiclePricePerDay = 32.50m,
                TotalPrice = 162.50m,
                Status = RentalStatus.Active
            },
            new()
            {
                Id = Guid.Parse("7bb21c7b-d0d8-4de0-8d84-2e8d4bffec85"),
                VehicleId = Guid.Parse("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"),
                RenterId = Guid.Parse("e7df3bc2-1c20-4895-b8c9-781ad6cf892a"),
                BookedOn = new DateTime(2025, 6, 5),
                StartDate = new DateTime(2025, 7, 20),
                EndDate = new DateTime(2025, 7, 25),
                VehiclePricePerDay = 32.50m,
                TotalPrice = 162.50m,
                Status = RentalStatus.Active
            },

            // Jeep Grand Cherokee
            new()
            {
                Id = Guid.Parse("b2c98c1a-45f4-4b89-9a74-51cfa684b9e2"),
                VehicleId = Guid.Parse("fe15cde2-1a90-46d4-89f1-10fda7f11743"),
                RenterId = Guid.Parse("d56b4e71-7c38-4c3f-8c85-ff2b7cfd2f01"),
                BookedOn = new DateTime(2025, 6, 10),
                StartDate = new DateTime(2025, 7, 1),
                EndDate = new DateTime(2025, 7, 7),
                VehiclePricePerDay = 62.00m,
                TotalPrice = 372.00m,
                Status = RentalStatus.Completed,
                CompletedOn = new DateTime(2025, 7, 7)
            },
            new()
            {
                Id = Guid.Parse("a5f61b5a-883e-47f4-8189-44b197967d5f"),
                VehicleId = Guid.Parse("fe15cde2-1a90-46d4-89f1-10fda7f11743"),
                RenterId = Guid.Parse("e7df3bc2-1c20-4895-b8c9-781ad6cf892a"),
                BookedOn = new DateTime(2025, 6, 15),
                StartDate = new DateTime(2025, 8, 10),
                EndDate = new DateTime(2025, 8, 15),
                VehiclePricePerDay = 62.00m,
                TotalPrice = 310.00m,
                Status = RentalStatus.Active
            },
            new()
            {
                Id = Guid.Parse("f62e7790-609b-4a42-a01c-fbb6d8c88f89"),
                VehicleId = Guid.Parse("fe15cde2-1a90-46d4-89f1-10fda7f11743"),
                RenterId = Guid.Parse("d56b4e71-7c38-4c3f-8c85-ff2b7cfd2f01"),
                BookedOn = new DateTime(2025, 6, 20),
                StartDate = new DateTime(2025, 8, 20),
                EndDate = new DateTime(2025, 8, 25),
                VehiclePricePerDay = 62.00m,
                TotalPrice = 310.00m,
                Status = RentalStatus.Active
            },

            // VW Golf
            new()
            {
                Id = Guid.Parse("dca1c233-b01b-4f6c-a0fc-f6b709bd92ef"),
                VehicleId = Guid.Parse("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"),
                RenterId = Guid.Parse("e7df3bc2-1c20-4895-b8c9-781ad6cf892a"),
                BookedOn = new DateTime(2025, 6, 3),
                StartDate = new DateTime(2025, 7, 5),
                EndDate = new DateTime(2025, 7, 10),
                VehiclePricePerDay = 54.00m,
                TotalPrice = 270.00m,
                Status = RentalStatus.Completed,
                CompletedOn = new DateTime(2025, 7, 10)
            },
            new()
            {
                Id = Guid.Parse("c63cc240-39c6-4d55-b5cf-bd17912825fc"),
                VehicleId = Guid.Parse("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"),
                RenterId = Guid.Parse("d56b4e71-7c38-4c3f-8c85-ff2b7cfd2f01"),
                BookedOn = new DateTime(2025, 6, 10),
                StartDate = new DateTime(2025, 7, 15),
                EndDate = new DateTime(2025, 7, 20),
                VehiclePricePerDay = 54.00m,
                TotalPrice = 270.00m,
                Status = RentalStatus.Active
            },
            new()
            {
                Id = Guid.Parse("f471e13c-5fae-4421-9fa4-300b08b97c28"),
                VehicleId = Guid.Parse("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"),
                RenterId = Guid.Parse("d56b4e71-7c38-4c3f-8c85-ff2b7cfd2f01"),
                BookedOn = new DateTime(2025, 6, 18),
                StartDate = new DateTime(2025, 7, 25),
                EndDate = new DateTime(2025, 7, 30),
                VehiclePricePerDay = 54.00m,
                TotalPrice = 270.00m,
                Status = RentalStatus.Active
            }
        };

            return rentals;
        }
    }

}
