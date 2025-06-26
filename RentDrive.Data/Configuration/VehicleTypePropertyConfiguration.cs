using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentDrive.Common.Enums;
using RentDrive.Data.Models;
using static RentDrive.Common.Vehicle.VehicleValidationConstants.VehicleTypeProperty;

namespace RentDrive.Data.Configuration
{
    public class VehicleTypePropertyConfiguration : IEntityTypeConfiguration<VehicleTypeProperty>
    {
        public void Configure(EntityTypeBuilder<VehicleTypeProperty> builder)
        {
            builder
                .HasKey(vtp => vtp.Id);

            builder
                .HasOne(vtp => vtp.VehicleType)
                .WithMany(vt => vt.VehicleTypeProperty)
                .HasForeignKey(vtp => vtp.VehicleTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(vtp => vtp.Name)
                .HasMaxLength(NameMaxLength)
                .IsRequired()
                .HasComment("Property Name");

            builder
                .Property(vtp => vtp.ValueType)
                .IsRequired()
                .HasComment("Property value type")
                .HasDefaultValue(PropertyValueType.String);

            builder
                .Property(vtp => vtp.UnitOfMeasurement)
                .IsRequired()
                .HasComment("Property unit of measurement")
                .HasDefaultValue(UnitOfMeasurement.None);
        }
    }
    public static class VehicleTypePropertySeeder
    {
        public static IEnumerable<VehicleTypeProperty> SeedVehicleTypeProperties()
        {
            IEnumerable<VehicleTypeProperty> vehicleTypeProperties = new List<VehicleTypeProperty>()
            {
                new()
                {
                   Id = Guid.Parse("671fbc1d-53ef-4862-9f73-974b94ac0d25"),
                   VehicleTypeId = 1,
                   Name = "Engine Displacement",
                   ValueType = PropertyValueType.Int,
                   UnitOfMeasurement = UnitOfMeasurement.cc
                },
                new()
                {
                   Id = Guid.Parse("13aaad5c-624c-4361-a863-6ff301f5c63d"),
                   VehicleTypeId = 1,
                   Name = "Door Count",
                   ValueType = PropertyValueType.Int,
                   UnitOfMeasurement = UnitOfMeasurement.None,
                },
                new()
                {
                   Id = Guid.Parse("77ab0d68-a40a-413a-9b4a-aa30716609db"),
                   VehicleTypeId = 1,
                   Name = "Seat Count",
                   ValueType = PropertyValueType.Int,
                   UnitOfMeasurement = UnitOfMeasurement.None,
                },
                new()
                {
                   Id = Guid.Parse("dd348516-a1dc-4336-b6c8-fb885b7afd0f"),
                   VehicleTypeId = 1,
                   Name = "Power in KiloWatts",
                   ValueType = PropertyValueType.Double,
                   UnitOfMeasurement = UnitOfMeasurement.kW,
                },
                new()
                {
                   Id = Guid.Parse("b924497e-006a-4d20-899f-66fde6c94ec8"),
                   VehicleTypeId = 3,
                   Name = "Engine Displacement",
                   ValueType = PropertyValueType.Int,
                   UnitOfMeasurement = UnitOfMeasurement.cc
                },
                new()
                {
                   Id = Guid.Parse("6b197695-891b-492a-8935-a54a500742c2"),
                   VehicleTypeId = 3,
                   Name = "Power in KiloWatts",
                   ValueType = PropertyValueType.Double,
                   UnitOfMeasurement = UnitOfMeasurement.kW,
                }
            };

            return vehicleTypeProperties;
        }
    }
}