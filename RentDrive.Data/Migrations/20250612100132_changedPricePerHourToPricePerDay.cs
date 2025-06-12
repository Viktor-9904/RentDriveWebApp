using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentDrive.Data.Migrations
{
    /// <inheritdoc />
    public partial class changedPricePerHourToPricePerDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerHour",
                table: "Vehicles");

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerDay",
                table: "Vehicles",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "Price per day for renting the vehicle.");

            migrationBuilder.AlterColumn<string>(
                name: "ImageURL",
                table: "VehicleImages",
                type: "nvarchar(2083)",
                maxLength: 2083,
                nullable: false,
                defaultValue: "images/default-image.jpg",
                comment: "Vehicle Image URL.",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldDefaultValue: "images/default-image.jpg",
                oldComment: "Vehicle Image URL.");

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"),
                column: "PricePerDay",
                value: 32.50m);

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("fe15cde2-1a90-46d4-89f1-10fda7f11743"),
                column: "PricePerDay",
                value: 62.00m);

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"),
                column: "PricePerDay",
                value: 54.00m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerDay",
                table: "Vehicles");

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerHour",
                table: "Vehicles",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "Price per hour for renting the vehicle.");

            migrationBuilder.AlterColumn<string>(
                name: "ImageURL",
                table: "VehicleImages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "images/default-image.jpg",
                comment: "Vehicle Image URL.",
                oldClrType: typeof(string),
                oldType: "nvarchar(2083)",
                oldMaxLength: 2083,
                oldDefaultValue: "images/default-image.jpg",
                oldComment: "Vehicle Image URL.");

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"),
                column: "PricePerHour",
                value: 12.50m);

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("fe15cde2-1a90-46d4-89f1-10fda7f11743"),
                column: "PricePerHour",
                value: 22.00m);

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"),
                column: "PricePerHour",
                value: 10.00m);
        }
    }
}
