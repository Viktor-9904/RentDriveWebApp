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
}
