using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RentDrive.Data.Models;
using static RentDrive.Common.EntityValidationConstants.VehicleImages;

namespace RentDrive.Data.Configuration
{
    public class VehicleImagesConfiguration : IEntityTypeConfiguration<VehicleImages>
    {
        public void Configure(EntityTypeBuilder<VehicleImages> builder)
        {
            builder
                .HasKey(vi => vi.Id);

            builder
                .HasOne(vi => vi.Vehicle)
                .WithMany(v => v.VehicleImages)
                .HasForeignKey(vi => vi.VehicleId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(vi => vi.ImageURL)
                .HasMaxLength(ImageURLMaxLength)
                .IsRequired()
                .HasComment("Vehicle Image URL.")
                .HasDefaultValue(DefaultImageURL);
        }
    }
}
