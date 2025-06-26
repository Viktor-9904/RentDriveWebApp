using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentDrive.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedEngineDisplacementAndPowerInKiloWattsToMotorcycle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "VehicleTypeProperties",
                columns: new[] { "Id", "Name", "UnitOfMeasurement", "ValueType", "VehicleTypeId" },
                values: new object[,]
                {
                    { new Guid("6b197695-891b-492a-8935-a54a500742c2"), "Power in KiloWatts", 8, 2, 3 },
                    { new Guid("b924497e-006a-4d20-899f-66fde6c94ec8"), "Engine Displacement", 5, 1, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "VehicleTypeProperties",
                keyColumn: "Id",
                keyValue: new Guid("6b197695-891b-492a-8935-a54a500742c2"));

            migrationBuilder.DeleteData(
                table: "VehicleTypeProperties",
                keyColumn: "Id",
                keyValue: new Guid("b924497e-006a-4d20-899f-66fde6c94ec8"));
        }
    }
}
