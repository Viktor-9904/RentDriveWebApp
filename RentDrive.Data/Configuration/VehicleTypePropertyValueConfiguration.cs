using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RentDrive.Data.Models;
using static RentDrive.Common.Vehicle.VehicleValidationConstants.VehicleTypePropertyValue;

namespace RentDrive.Data.Configuration
{
    public class VehicleTypePropertyValueConfiguration : IEntityTypeConfiguration<VehicleTypePropertyValue>
    {
        public void Configure(EntityTypeBuilder<VehicleTypePropertyValue> builder)
        {
            builder
                .HasKey(vtpv => vtpv.Id);

            builder
                .HasOne(vtpv => vtpv.Vehicle)
                .WithMany(vtp => vtp.VehicleTypePropertyValues)
                .HasForeignKey(vtpv => vtpv.VehicleId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(vtpv => vtpv.VehicleTypeProperty)
                .WithMany(vtp => vtp.VehicleTypePropertyValues)
                .HasForeignKey(vtpv => vtpv.VehicleTypePropertyId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(vtpv => vtpv.PropertyValue)
                .IsRequired()
                .HasMaxLength(PropertyValueMaxLength)
                .HasComment("Property Value");

            builder
                .HasIndex(vtpv => new { vtpv.VehicleId, vtpv.VehicleTypePropertyId })
                .IsUnique();
        }
    }
}
