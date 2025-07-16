using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentDrive.Data.Migrations
{
    /// <inheritdoc />
    public partial class ImportSeeders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "VehicleTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Car" },
                    { 2, "Truck" },
                    { 3, "Motorcycle" },
                    { 4, "Bicycle" },
                    { 5, "Electric Scooter" },
                    { 6, "All Terrain Vehicle (ATV)" },
                    { 7, "Camper Trailer" },
                    { 8, "Recreational Vehicle" },
                    { 9, "Limousine" }
                });

            migrationBuilder.InsertData(
                table: "VehicleTypeCategories",
                columns: new[] { "Id", "Description", "Name", "VehicleTypeId" },
                values: new object[,]
                {
                    { 1, "Spacious and powerful car ideal for families and off-road.", "SUV", 1 },
                    { 2, "Comfortable passenger car suitable for everyday use.", "Sedan", 1 },
                    { 3, "Compact car with a rear door that swings upward.", "Hatchback", 1 },
                    { 4, "Truck with an open cargo area in the back.", "Pickup", 2 },
                    { 5, "Truck with a large, enclosed cargo area.", "Box Truck", 2 },
                    { 6, "Very good bike for everyday riding.", "Naked", 3 }
                });

            migrationBuilder.InsertData(
                table: "VehicleTypeProperties",
                columns: new[] { "Id", "Name", "ValueType", "VehicleTypeId" },
                values: new object[] { new Guid("13aaad5c-624c-4361-a863-6ff301f5c63d"), "Door Count", 1, 1 });

            migrationBuilder.InsertData(
                table: "VehicleTypeProperties",
                columns: new[] { "Id", "Name", "UnitOfMeasurement", "ValueType", "VehicleTypeId" },
                values: new object[,]
                {
                    { new Guid("671fbc1d-53ef-4862-9f73-974b94ac0d25"), "Engine Displacement", 5, 1, 1 },
                    { new Guid("6b197695-891b-492a-8935-a54a500742c2"), "Power in KiloWatts", 8, 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "VehicleTypeProperties",
                columns: new[] { "Id", "Name", "ValueType", "VehicleTypeId" },
                values: new object[] { new Guid("77ab0d68-a40a-413a-9b4a-aa30716609db"), "Seat Count", 1, 1 });

            migrationBuilder.InsertData(
                table: "VehicleTypeProperties",
                columns: new[] { "Id", "Name", "UnitOfMeasurement", "ValueType", "VehicleTypeId" },
                values: new object[,]
                {
                    { new Guid("b924497e-006a-4d20-899f-66fde6c94ec8"), "Engine Displacement", 5, 1, 3 },
                    { new Guid("dd348516-a1dc-4336-b6c8-fb885b7afd0f"), "Power in KiloWatts", 8, 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Color", "CurbWeightInKg", "DateAdded", "DateOfProduction", "Description", "FuelType", "Make", "Model", "OwnerId", "PricePerDay", "VehicleTypeCategoryId", "VehicleTypeId" },
                values: new object[,]
                {
                    { new Guid("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"), "White", 1470.0, new DateTime(2022, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comfortable midsize sedan, ideal for long drives.", 1, "Toyota", "Camry", null, 32.50m, 2, 1 },
                    { new Guid("fe15cde2-1a90-46d4-89f1-10fda7f11743"), "Dark Green", 2045.0, new DateTime(2023, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Spacious and off-road capable SUV.", 2, "Jeep", "Grand Cherokee", null, 62.00m, 1, 1 },
                    { new Guid("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"), "Silver", 1300.0, new DateTime(2024, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Compact hatchback with great fuel economy.", 1, "Volkswagen", "Golf", null, 54.00m, 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "VehicleImages",
                columns: new[] { "Id", "ImageURL", "VehicleId" },
                values: new object[,]
                {
                    { new Guid("1d989d71-afdb-4741-8851-dedc44ffe964"), "images/vehicles/Vehicle-2.jpg", new Guid("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4") },
                    { new Guid("47725763-dc3a-485b-9f12-26df29497dd1"), "images/vehicles/Vehicle-3.jpg", new Guid("fe15cde2-1a90-46d4-89f1-10fda7f11743") },
                    { new Guid("a35616f3-67b2-4d66-95cd-78fae80883fa"), "images/vehicles/Vehicle-1.png", new Guid("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de") }
                });

            migrationBuilder.InsertData(
                table: "VehicleTypePropertyValues",
                columns: new[] { "Id", "PropertyValue", "VehicleId", "VehicleTypePropertyId" },
                values: new object[,]
                {
                    { new Guid("226d11c2-6ace-482d-8c05-052ae6a36df5"), "5", new Guid("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"), new Guid("13aaad5c-624c-4361-a863-6ff301f5c63d") },
                    { new Guid("43049eae-e00f-48c0-94a1-dbcd66f1b892"), "218", new Guid("fe15cde2-1a90-46d4-89f1-10fda7f11743"), new Guid("dd348516-a1dc-4336-b6c8-fb885b7afd0f") },
                    { new Guid("4bdc05a2-559f-412c-994a-69c1ac6ab24f"), "2487", new Guid("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"), new Guid("671fbc1d-53ef-4862-9f73-974b94ac0d25") },
                    { new Guid("53c60eee-041b-4cf7-828f-43c0c3000305"), "4", new Guid("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"), new Guid("13aaad5c-624c-4361-a863-6ff301f5c63d") },
                    { new Guid("838bb66e-a194-4b05-a35d-397746dbc375"), "7", new Guid("fe15cde2-1a90-46d4-89f1-10fda7f11743"), new Guid("77ab0d68-a40a-413a-9b4a-aa30716609db") },
                    { new Guid("92820a2c-2324-4279-aff9-8e606f868200"), "3604", new Guid("fe15cde2-1a90-46d4-89f1-10fda7f11743"), new Guid("671fbc1d-53ef-4862-9f73-974b94ac0d25") },
                    { new Guid("c9951dc1-d840-42b4-854e-44104883613f"), "5", new Guid("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"), new Guid("77ab0d68-a40a-413a-9b4a-aa30716609db") },
                    { new Guid("deb2022f-58a6-46de-93de-78266eb453b2"), "110", new Guid("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"), new Guid("dd348516-a1dc-4336-b6c8-fb885b7afd0f") },
                    { new Guid("dfd548e3-0542-435e-b80e-b349a0e86c79"), "1395", new Guid("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"), new Guid("671fbc1d-53ef-4862-9f73-974b94ac0d25") },
                    { new Guid("e0c89c2c-28ad-496d-bee9-17da515dee11"), "151", new Guid("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"), new Guid("dd348516-a1dc-4336-b6c8-fb885b7afd0f") },
                    { new Guid("e400d83c-9533-426f-b6ce-5916ce24f510"), "5", new Guid("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"), new Guid("77ab0d68-a40a-413a-9b4a-aa30716609db") },
                    { new Guid("ea03bee5-44ac-4140-90e4-5932694920b7"), "4", new Guid("fe15cde2-1a90-46d4-89f1-10fda7f11743"), new Guid("13aaad5c-624c-4361-a863-6ff301f5c63d") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "VehicleImages",
                keyColumn: "Id",
                keyValue: new Guid("1d989d71-afdb-4741-8851-dedc44ffe964"));

            migrationBuilder.DeleteData(
                table: "VehicleImages",
                keyColumn: "Id",
                keyValue: new Guid("47725763-dc3a-485b-9f12-26df29497dd1"));

            migrationBuilder.DeleteData(
                table: "VehicleImages",
                keyColumn: "Id",
                keyValue: new Guid("a35616f3-67b2-4d66-95cd-78fae80883fa"));

            migrationBuilder.DeleteData(
                table: "VehicleTypeCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "VehicleTypeCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "VehicleTypeCategories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "VehicleTypeProperties",
                keyColumn: "Id",
                keyValue: new Guid("6b197695-891b-492a-8935-a54a500742c2"));

            migrationBuilder.DeleteData(
                table: "VehicleTypeProperties",
                keyColumn: "Id",
                keyValue: new Guid("b924497e-006a-4d20-899f-66fde6c94ec8"));

            migrationBuilder.DeleteData(
                table: "VehicleTypePropertyValues",
                keyColumn: "Id",
                keyValue: new Guid("226d11c2-6ace-482d-8c05-052ae6a36df5"));

            migrationBuilder.DeleteData(
                table: "VehicleTypePropertyValues",
                keyColumn: "Id",
                keyValue: new Guid("43049eae-e00f-48c0-94a1-dbcd66f1b892"));

            migrationBuilder.DeleteData(
                table: "VehicleTypePropertyValues",
                keyColumn: "Id",
                keyValue: new Guid("4bdc05a2-559f-412c-994a-69c1ac6ab24f"));

            migrationBuilder.DeleteData(
                table: "VehicleTypePropertyValues",
                keyColumn: "Id",
                keyValue: new Guid("53c60eee-041b-4cf7-828f-43c0c3000305"));

            migrationBuilder.DeleteData(
                table: "VehicleTypePropertyValues",
                keyColumn: "Id",
                keyValue: new Guid("838bb66e-a194-4b05-a35d-397746dbc375"));

            migrationBuilder.DeleteData(
                table: "VehicleTypePropertyValues",
                keyColumn: "Id",
                keyValue: new Guid("92820a2c-2324-4279-aff9-8e606f868200"));

            migrationBuilder.DeleteData(
                table: "VehicleTypePropertyValues",
                keyColumn: "Id",
                keyValue: new Guid("c9951dc1-d840-42b4-854e-44104883613f"));

            migrationBuilder.DeleteData(
                table: "VehicleTypePropertyValues",
                keyColumn: "Id",
                keyValue: new Guid("deb2022f-58a6-46de-93de-78266eb453b2"));

            migrationBuilder.DeleteData(
                table: "VehicleTypePropertyValues",
                keyColumn: "Id",
                keyValue: new Guid("dfd548e3-0542-435e-b80e-b349a0e86c79"));

            migrationBuilder.DeleteData(
                table: "VehicleTypePropertyValues",
                keyColumn: "Id",
                keyValue: new Guid("e0c89c2c-28ad-496d-bee9-17da515dee11"));

            migrationBuilder.DeleteData(
                table: "VehicleTypePropertyValues",
                keyColumn: "Id",
                keyValue: new Guid("e400d83c-9533-426f-b6ce-5916ce24f510"));

            migrationBuilder.DeleteData(
                table: "VehicleTypePropertyValues",
                keyColumn: "Id",
                keyValue: new Guid("ea03bee5-44ac-4140-90e4-5932694920b7"));

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "VehicleTypeProperties",
                keyColumn: "Id",
                keyValue: new Guid("13aaad5c-624c-4361-a863-6ff301f5c63d"));

            migrationBuilder.DeleteData(
                table: "VehicleTypeProperties",
                keyColumn: "Id",
                keyValue: new Guid("671fbc1d-53ef-4862-9f73-974b94ac0d25"));

            migrationBuilder.DeleteData(
                table: "VehicleTypeProperties",
                keyColumn: "Id",
                keyValue: new Guid("77ab0d68-a40a-413a-9b4a-aa30716609db"));

            migrationBuilder.DeleteData(
                table: "VehicleTypeProperties",
                keyColumn: "Id",
                keyValue: new Guid("dd348516-a1dc-4336-b6c8-fb885b7afd0f"));

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("fe15cde2-1a90-46d4-89f1-10fda7f11743"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"));

            migrationBuilder.DeleteData(
                table: "VehicleTypeCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VehicleTypeCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "VehicleTypeCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
