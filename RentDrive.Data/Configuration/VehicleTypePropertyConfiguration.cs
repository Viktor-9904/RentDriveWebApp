using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RentDrive.Data.Models;
using RentDrive.Data.Models.Enums;
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
}