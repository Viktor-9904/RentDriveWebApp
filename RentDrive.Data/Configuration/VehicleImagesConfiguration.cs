using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentDrive.Data.Models;
using static RentDrive.Common.Vehicle.VehicleValidationConstants.VehicleImages;

namespace RentDrive.Data.Configuration
{
    public class VehicleImagesConfiguration : IEntityTypeConfiguration<VehicleImage>
    {
        public void Configure(EntityTypeBuilder<VehicleImage> builder)
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
    public static class VehicleImageSeeder
    {
        public static IEnumerable<VehicleImage> SeedVehicleImages()
        {
            IEnumerable<VehicleImage> vehicleImages = new List<VehicleImage>()
                {
                    new()
                    {
                        Id = new Guid("a35616f3-67b2-4d66-95cd-78fae80883fa"),
                        VehicleId = new Guid("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"),
                        ImageURL = "images/vehicles/Vehicle-1.png"
                    },
                    new()
                    {
                        Id = new Guid("47725763-dc3a-485b-9f12-26df29497dd1"),
                        VehicleId = new Guid("fe15cde2-1a90-46d4-89f1-10fda7f11743"),
                        ImageURL = "images/vehicles/Vehicle-3.jpg"
                    },
                    new()
                    {
                        Id = new Guid("1d989d71-afdb-4741-8851-dedc44ffe964"),
                        VehicleId = new Guid("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"),
                        ImageURL = "images/vehicles/Vehicle-2.jpg"
                    },
                };
            return vehicleImages;
        }
    }
}
