using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RentDrive.Data.Models;
using static RentDrive.Common.EntityValidationConstants.VehicleValidationConstants.VehicleReview;

namespace RentDrive.Data.Configuration
{
    public class VehicleReviewConfiguration : IEntityTypeConfiguration<VehicleReview>
    {
        public void Configure(EntityTypeBuilder<VehicleReview> builder)
        {
            builder
                .HasKey(vr => vr.Id);

            builder
                .HasOne(vr => vr.Vehicle)
                .WithMany(v => v.Reviews)
                .HasForeignKey(vr => vr.VehicleId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(vr => vr.Rental)
                .WithOne(r => r.Review)
                .HasForeignKey<VehicleReview>(vr => vr.RentalId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasIndex(vr => vr.RentalId)
                .IsUnique();

            builder
                .HasOne(vr => vr.Reviewer)
                .WithMany(au => au.ReviewsGiven)
                .HasForeignKey(vr => vr.ReviewerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(vr => vr.Stars)
                .IsRequired()
                .HasComment("Vehicle rating scale from 0 to 10.");

            builder
                .Property(vr => vr.Comment)
                .HasMaxLength(CommentMaxLength)
                .HasComment("Review Comment");

            builder
                .Property(vr => vr.CreatedOn)
                .IsRequired()
                .HasComment("Review creation date.");
        }
    }
    public static class VehicleReviewSeeder
    {
        public static IEnumerable<VehicleReview> SeedVehicleReviews()
        {
            IEnumerable<VehicleReview> reviews = new List<VehicleReview>
        {
            // Toyota Camry Reviews
            new()
            {
                Id = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                VehicleId = Guid.Parse("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"),
                RentalId = Guid.Parse("a1a25e78-2b30-4f77-a899-08db1682a00a"),
                ReviewerId = Guid.Parse("d56b4e71-7c38-4c3f-8c85-ff2b7cfd2f01"),
                Stars = 5,
                Comment = "Smooth ride, very clean, great experience!",
                CreatedOn = new DateTime(2025, 1, 7)
            },
            new()
            {
                Id = Guid.Parse("22222222-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                VehicleId = Guid.Parse("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"),
                RentalId = Guid.Parse("9cfe3db6-50e1-41e4-8a98-3cdba63c20b1"),
                ReviewerId = Guid.Parse("e7df3bc2-1c20-4895-b8c9-781ad6cf892a"),
                Stars = 2,
                Comment = "Engine was louder than expected, not well maintained.",
                CreatedOn = new DateTime(2025, 7, 16)
            },

            // Jeep Grand Cherokee Reviews
            new()
            {
                Id = Guid.Parse("33333333-cccc-cccc-cccc-cccccccccccc"),
                VehicleId = Guid.Parse("fe15cde2-1a90-46d4-89f1-10fda7f11743"),
                RentalId = Guid.Parse("b2c98c1a-45f4-4b89-9a74-51cfa684b9e2"),
                ReviewerId = Guid.Parse("d56b4e71-7c38-4c3f-8c85-ff2b7cfd2f01"),
                Stars = 4,
                Comment = "Powerful SUV, great for the mountains.",
                CreatedOn = new DateTime(2025, 7, 8)
            },
            new()
            {
                Id = Guid.Parse("44444444-dddd-dddd-dddd-dddddddddddd"),
                VehicleId = Guid.Parse("fe15cde2-1a90-46d4-89f1-10fda7f11743"),
                RentalId = Guid.Parse("a5f61b5a-883e-47f4-8189-44b197967d5f"),
                ReviewerId = Guid.Parse("e7df3bc2-1c20-4895-b8c9-781ad6cf892a"),
                Stars = 1,
                Comment = "Uncomfortable seats and poor air conditioning.",
                CreatedOn = new DateTime(2025, 8, 16)
            },

            // VW Golf Reviews
            new()
            {
                Id = Guid.Parse("55555555-eeee-eeee-eeee-eeeeeeeeeeee"),
                VehicleId = Guid.Parse("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"),
                RentalId = Guid.Parse("dca1c233-b01b-4f6c-a0fc-f6b709bd92ef"),
                ReviewerId = Guid.Parse("e7df3bc2-1c20-4895-b8c9-781ad6cf892a"),
                Stars = 3,
                Comment = "Decent for city driving, but not spacious.",
                CreatedOn = new DateTime(2025, 7, 11)
            },
            new()
            {
                Id = Guid.Parse("66666666-ffff-ffff-ffff-ffffffffffff"),
                VehicleId = Guid.Parse("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"),
                RentalId = Guid.Parse("c63cc240-39c6-4d55-b5cf-bd17912825fc"),
                ReviewerId = Guid.Parse("d56b4e71-7c38-4c3f-8c85-ff2b7cfd2f01"),
                Stars = 5,
                Comment = "Very fuel efficient and easy to park!",
                CreatedOn = new DateTime(2025, 7, 21)
            }
        };

            return reviews;
        }
    }
}
