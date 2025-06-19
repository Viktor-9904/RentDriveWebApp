using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentDrive.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedVehicleTypePropertySeederAndVehicleTypePropertyValueSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FuelType",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Vehicle fuel type.");

            migrationBuilder.CreateTable(
                name: "VehicleTypeProperty",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Property Name"),
                    ValueType = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "Property value type"),
                    UnitOfMeasurement = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "Property unit of measurement")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypeProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleTypeProperty_VehicleTypes_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalTable: "VehicleTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VehicleTypePropertyValue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleTypePropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Property Value")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypePropertyValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleTypePropertyValue_VehicleTypeProperty_VehicleTypePropertyId",
                        column: x => x.VehicleTypePropertyId,
                        principalTable: "VehicleTypeProperty",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VehicleTypePropertyValue_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "VehicleTypeProperty",
                columns: new[] { "Id", "Name", "ValueType", "VehicleTypeId" },
                values: new object[] { new Guid("13aaad5c-624c-4361-a863-6ff301f5c63d"), "Door Count", 1, 1 });

            migrationBuilder.InsertData(
                table: "VehicleTypeProperty",
                columns: new[] { "Id", "Name", "UnitOfMeasurement", "ValueType", "VehicleTypeId" },
                values: new object[] { new Guid("671fbc1d-53ef-4862-9f73-974b94ac0d25"), "Cubic Centimeters", 5, 1, 1 });

            migrationBuilder.InsertData(
                table: "VehicleTypeProperty",
                columns: new[] { "Id", "Name", "ValueType", "VehicleTypeId" },
                values: new object[] { new Guid("77ab0d68-a40a-413a-9b4a-aa30716609db"), "Seat Count", 1, 1 });

            migrationBuilder.InsertData(
                table: "VehicleTypeProperty",
                columns: new[] { "Id", "Name", "UnitOfMeasurement", "ValueType", "VehicleTypeId" },
                values: new object[] { new Guid("dd348516-a1dc-4336-b6c8-fb885b7afd0f"), "Power in KiloWatts", 8, 2, 1 });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"),
                column: "FuelType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("fe15cde2-1a90-46d4-89f1-10fda7f11743"),
                column: "FuelType",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"),
                column: "FuelType",
                value: 1);

            migrationBuilder.InsertData(
                table: "VehicleTypePropertyValue",
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

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTypeProperty_VehicleTypeId",
                table: "VehicleTypeProperty",
                column: "VehicleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTypePropertyValue_VehicleId_VehicleTypePropertyId",
                table: "VehicleTypePropertyValue",
                columns: new[] { "VehicleId", "VehicleTypePropertyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTypePropertyValue_VehicleTypePropertyId",
                table: "VehicleTypePropertyValue",
                column: "VehicleTypePropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleTypePropertyValue");

            migrationBuilder.DropTable(
                name: "VehicleTypeProperty");

            migrationBuilder.DropColumn(
                name: "FuelType",
                table: "Vehicles");
        }
    }
}
