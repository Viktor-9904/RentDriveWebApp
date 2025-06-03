using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentDrive.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedVehicleImageSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "VehicleImages",
                columns: new[] { "Id", "ImageURL", "VehicleId" },
                values: new object[,]
                {
                    { new Guid("1d989d71-afdb-4741-8851-dedc44ffe964"), "/images/vehicles/Vehicle-2.jpg", new Guid("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4") },
                    { new Guid("47725763-dc3a-485b-9f12-26df29497dd1"), "/images/vehicles/Vehicle-3.jpg", new Guid("fe15cde2-1a90-46d4-89f1-10fda7f11743") },
                    { new Guid("a35616f3-67b2-4d66-95cd-78fae80883fa"), "/images/vehicles/Vehicle-1.png", new Guid("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de") }
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
        }
    }
}
