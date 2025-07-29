using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentDrive.Data.Models;
using static RentDrive.Common.EntityValidationConstants.VehicleValidationConstants.VehicleTypeClass;

namespace RentDrive.Data.Configuration
{
    public class VehicleTypeCategoryConfiguration : IEntityTypeConfiguration<VehicleTypeCategory>
    {
        public void Configure(EntityTypeBuilder<VehicleTypeCategory> builder)
        {
            builder
                .HasKey(vtc => vtc.Id);

            builder
                .HasOne(vtc => vtc.VehicleType)
                .WithMany(vt => vt.VehicleTypeCategory)
                .HasForeignKey(vtc => vtc.VehicleTypeId);

            builder
                .Property(vtc => vtc.Name)
                .HasMaxLength(NameMaxLength)
                .IsRequired()
                .HasComment("Name of the vehicle class.");

            builder
                .Property(vtc => vtc.Description)
                .HasMaxLength(DescriptionMaxLength)
                .IsRequired()
                .HasComment("Description of the vehicle class.");

            builder
                .Property(vtc => vtc.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false)
                .HasComment("Is vehicle type category soft deleted.");

            builder
                .HasIndex(vtc => new { vtc.VehicleTypeId, vtc.Name })
                .IsUnique();
        }
    }
    public static class VehicleTypeCategorySeeder
    {
        public static IEnumerable<VehicleTypeCategory> SeedVehicleTypeCategories()
        {
            IEnumerable<VehicleTypeCategory> vehicleTypeCategory = new List<VehicleTypeCategory>()
            {
                new()
                {
                    Id = 1,
                    VehicleTypeId = 1,
                    Name = "SUV",
                    Description = "Spacious and powerful car ideal for families and off-road.",
                    IsDeleted = false,
                },
                new()
                {
                    Id = 2,
                    VehicleTypeId = 1,
                    Name = "Sedan",
                    Description = "Comfortable passenger car suitable for everyday use.",
                    IsDeleted = false,
                },
                new()
                {
                    Id = 3,
                    VehicleTypeId = 1,
                    Name = "Hatchback",
                    Description = "Compact car with a rear door that swings upward.",
                    IsDeleted = false,
                },
                new()
                {
                    Id = 4,
                    VehicleTypeId = 2,
                    Name = "Pickup",
                    Description = "Truck with an open cargo area in the back.",
                    IsDeleted = false,
                },
                new()
                {
                    Id = 5,
                    VehicleTypeId = 2,
                    Name = "Box Truck",
                    Description = "Truck with a large, enclosed cargo area.",
                    IsDeleted = false,
                },
                new()
                {
                    Id = 6,
                    VehicleTypeId = 3,
                    Name = "Naked",
                    Description = "Very good bike for everyday riding.",
                    IsDeleted = false,
                },
            };
            return vehicleTypeCategory;
        }
    }
}
