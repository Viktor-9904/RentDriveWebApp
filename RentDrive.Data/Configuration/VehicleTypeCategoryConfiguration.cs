using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RentDrive.Data.Models;
using static RentDrive.Common.EntityValidationConstants.VehicleTypeClass;

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
                .Property(vtc => vtc.CategoryName)
                .HasMaxLength(NameMaxLength)
                .IsRequired()
                .HasComment("Name of the vehicle class.");

            builder
                .Property(vtc => vtc.Description)
                .HasMaxLength(DescriptionMaxLength)
                .HasComment("Description of the vehicle class.");
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
                    CategoryName = "SUV",
                    Description = "Spacious and powerful car ideal for families and off-road.",
                },
                new()
                {
                    Id = 2,
                    VehicleTypeId = 1,
                    CategoryName = "Sedan",
                    Description = "Comfortable passenger car suitable for everyday use.",
                },
                new()
                {
                    Id = 3,
                    VehicleTypeId = 1,
                    CategoryName = "Hatchback",
                    Description = "Compact car with a rear door that swings upward.",
                },
                new()
                {
                    Id = 4,
                    VehicleTypeId = 2,
                    CategoryName = "Pickup",
                    Description = "Truck with an open cargo area in the back.",
                },
                new()
                {
                    Id = 5,
                    VehicleTypeId = 2,
                    CategoryName = "Box Truck",
                    Description = "Truck with a large, enclosed cargo area.",
                },
                new()
                {
                    Id = 6,
                    VehicleTypeId = 3,
                    CategoryName = "Naked",
                    Description = "Very good bike for everyday riding.",
                },
            };
            return vehicleTypeCategory;
        }
    }
}
