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
    public static class VehicleTypePropertyValueSeerder
    {
        public static IEnumerable<VehicleTypePropertyValue> SeedVehicleTypePropertyValues()
        {
            IEnumerable<VehicleTypePropertyValue> vehicleTypePropertyValues = new List<VehicleTypePropertyValue>()
            {
                // Toyota
                new()
                {
                    Id = Guid.Parse("4bdc05a2-559f-412c-994a-69c1ac6ab24f"),
                    VehicleId = Guid.Parse("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"),
                    VehicleTypePropertyId = Guid.Parse("671fbc1d-53ef-4862-9f73-974b94ac0d25"),
                    PropertyValue = "2487"
                },
                new()
                {
                    Id = Guid.Parse("53c60eee-041b-4cf7-828f-43c0c3000305"),
                    VehicleId = Guid.Parse("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"),
                    VehicleTypePropertyId = Guid.Parse("13aaad5c-624c-4361-a863-6ff301f5c63d"),
                    PropertyValue = "4"
                },
                new()
                {
                    Id = Guid.Parse("c9951dc1-d840-42b4-854e-44104883613f"),
                    VehicleId = Guid.Parse("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"),
                    VehicleTypePropertyId = Guid.Parse("77ab0d68-a40a-413a-9b4a-aa30716609db"),
                    PropertyValue = "5"
                },
                new()
                {
                    Id = Guid.Parse("e0c89c2c-28ad-496d-bee9-17da515dee11"),
                    VehicleId = Guid.Parse("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"),
                    VehicleTypePropertyId = Guid.Parse("dd348516-a1dc-4336-b6c8-fb885b7afd0f"),
                    PropertyValue = "151"
                },
                // Jeep
                new()
                {
                    Id = Guid.Parse("92820a2c-2324-4279-aff9-8e606f868200"),
                    VehicleId = Guid.Parse("fe15cde2-1a90-46d4-89f1-10fda7f11743"),
                    VehicleTypePropertyId = Guid.Parse("671fbc1d-53ef-4862-9f73-974b94ac0d25"),
                    PropertyValue = "3604"
                },
                new()
                {
                    Id = Guid.Parse("ea03bee5-44ac-4140-90e4-5932694920b7"),
                    VehicleId = Guid.Parse("fe15cde2-1a90-46d4-89f1-10fda7f11743"),
                    VehicleTypePropertyId = Guid.Parse("13aaad5c-624c-4361-a863-6ff301f5c63d"),
                    PropertyValue = "4"
                },
                new()
                {
                    Id = Guid.Parse("838bb66e-a194-4b05-a35d-397746dbc375"),
                    VehicleId = Guid.Parse("fe15cde2-1a90-46d4-89f1-10fda7f11743"),
                    VehicleTypePropertyId = Guid.Parse("77ab0d68-a40a-413a-9b4a-aa30716609db"),
                    PropertyValue = "7"
                },
                new()
                {
                    Id = Guid.Parse("43049eae-e00f-48c0-94a1-dbcd66f1b892"),
                    VehicleId = Guid.Parse("fe15cde2-1a90-46d4-89f1-10fda7f11743"),
                    VehicleTypePropertyId = Guid.Parse("dd348516-a1dc-4336-b6c8-fb885b7afd0f"),
                    PropertyValue = "218"
                },
                // Volkswagen
                new()
                {
                    Id = Guid.Parse("dfd548e3-0542-435e-b80e-b349a0e86c79"),
                    VehicleId = Guid.Parse("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"),
                    VehicleTypePropertyId = Guid.Parse("671fbc1d-53ef-4862-9f73-974b94ac0d25"),
                    PropertyValue = "1395"
                },
                new()
                {
                    Id = Guid.Parse("226d11c2-6ace-482d-8c05-052ae6a36df5"),
                    VehicleId = Guid.Parse("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"),
                    VehicleTypePropertyId = Guid.Parse("13aaad5c-624c-4361-a863-6ff301f5c63d"),
                    PropertyValue = "5"
                },
                new()
                {
                    Id = Guid.Parse("e400d83c-9533-426f-b6ce-5916ce24f510"),
                    VehicleId = Guid.Parse("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"),
                    VehicleTypePropertyId = Guid.Parse("77ab0d68-a40a-413a-9b4a-aa30716609db"),
                    PropertyValue = "5"
                },
                new()
                {
                    Id = Guid.Parse("deb2022f-58a6-46de-93de-78266eb453b2"),
                    VehicleId = Guid.Parse("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"),
                    VehicleTypePropertyId = Guid.Parse("dd348516-a1dc-4336-b6c8-fb885b7afd0f"),
                    PropertyValue = "110"
                },
            };

            return vehicleTypePropertyValues;
        }
    }
}
