using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentDrive.Data.Migrations
{
    /// <inheritdoc />
    public partial class removedDashLineFromStaticVehicleImageSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "VehicleImages",
                keyColumn: "Id",
                keyValue: new Guid("1d989d71-afdb-4741-8851-dedc44ffe964"),
                column: "ImageURL",
                value: "images/vehicles/Vehicle-2.jpg");

            migrationBuilder.UpdateData(
                table: "VehicleImages",
                keyColumn: "Id",
                keyValue: new Guid("47725763-dc3a-485b-9f12-26df29497dd1"),
                column: "ImageURL",
                value: "images/vehicles/Vehicle-3.jpg");

            migrationBuilder.UpdateData(
                table: "VehicleImages",
                keyColumn: "Id",
                keyValue: new Guid("a35616f3-67b2-4d66-95cd-78fae80883fa"),
                column: "ImageURL",
                value: "images/vehicles/Vehicle-1.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "VehicleImages",
                keyColumn: "Id",
                keyValue: new Guid("1d989d71-afdb-4741-8851-dedc44ffe964"),
                column: "ImageURL",
                value: "/images/vehicles/Vehicle-2.jpg");

            migrationBuilder.UpdateData(
                table: "VehicleImages",
                keyColumn: "Id",
                keyValue: new Guid("47725763-dc3a-485b-9f12-26df29497dd1"),
                column: "ImageURL",
                value: "/images/vehicles/Vehicle-3.jpg");

            migrationBuilder.UpdateData(
                table: "VehicleImages",
                keyColumn: "Id",
                keyValue: new Guid("a35616f3-67b2-4d66-95cd-78fae80883fa"),
                column: "ImageURL",
                value: "/images/vehicles/Vehicle-1.png");
        }
    }
}
