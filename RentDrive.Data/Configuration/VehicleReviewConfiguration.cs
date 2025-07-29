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
                .Property(vr => vr.Rating)
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
}
