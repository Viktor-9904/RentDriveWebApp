using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentDrive.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedVehiclePricePerDayToRentalModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "VehiclePricePerDay",
                table: "Rentals",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "Vehicle price per day.");

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: new Guid("7bb21c7b-d0d8-4de0-8d84-2e8d4bffec85"),
                column: "VehiclePricePerDay",
                value: 32.50m);

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: new Guid("9cfe3db6-50e1-41e4-8a98-3cdba63c20b1"),
                column: "VehiclePricePerDay",
                value: 32.50m);

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: new Guid("a1a25e78-2b30-4f77-a899-08db1682a00a"),
                columns: new[] { "TotalPrice", "VehiclePricePerDay" },
                values: new object[] { 130.00m, 32.50m });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: new Guid("a5f61b5a-883e-47f4-8189-44b197967d5f"),
                columns: new[] { "TotalPrice", "VehiclePricePerDay" },
                values: new object[] { 310.00m, 62.00m });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: new Guid("b2c98c1a-45f4-4b89-9a74-51cfa684b9e2"),
                columns: new[] { "TotalPrice", "VehiclePricePerDay" },
                values: new object[] { 372.00m, 62.00m });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: new Guid("c63cc240-39c6-4d55-b5cf-bd17912825fc"),
                column: "VehiclePricePerDay",
                value: 54.00m);

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: new Guid("dca1c233-b01b-4f6c-a0fc-f6b709bd92ef"),
                column: "VehiclePricePerDay",
                value: 54.00m);

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: new Guid("f471e13c-5fae-4421-9fa4-300b08b97c28"),
                column: "VehiclePricePerDay",
                value: 54.00m);

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: new Guid("f62e7790-609b-4a42-a01c-fbb6d8c88f89"),
                columns: new[] { "TotalPrice", "VehiclePricePerDay" },
                values: new object[] { 310.00m, 62.00m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehiclePricePerDay",
                table: "Rentals");

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: new Guid("a1a25e78-2b30-4f77-a899-08db1682a00a"),
                column: "TotalPrice",
                value: 162.50m);

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: new Guid("a5f61b5a-883e-47f4-8189-44b197967d5f"),
                column: "TotalPrice",
                value: 372.00m);

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: new Guid("b2c98c1a-45f4-4b89-9a74-51cfa684b9e2"),
                column: "TotalPrice",
                value: 434.00m);

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: new Guid("f62e7790-609b-4a42-a01c-fbb6d8c88f89"),
                column: "TotalPrice",
                value: 372.00m);
        }
    }
}
