using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentDrive.Data.Migrations
{
    /// <inheritdoc />
    public partial class removedDepricatedVehicleProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EngineDisplacement",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "FuelType",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "OdoKilometers",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "PowerInKiloWatts",
                table: "Vehicles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "EngineDisplacement",
                table: "Vehicles",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                comment: "Vehicle's engine capacity in liters.");

            migrationBuilder.AddColumn<int>(
                name: "FuelType",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Vehicle's fuel type.");

            migrationBuilder.AddColumn<int>(
                name: "OdoKilometers",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Vehicle's total kilometers traveled.");

            migrationBuilder.AddColumn<double>(
                name: "PowerInKiloWatts",
                table: "Vehicles",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                comment: "Power output of the vehicle's engine in kilowatts.");

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"),
                columns: new[] { "EngineDisplacement", "FuelType", "OdoKilometers", "PowerInKiloWatts" },
                values: new object[] { 2.5, 1, 34500, 150.0 });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("fe15cde2-1a90-46d4-89f1-10fda7f11743"),
                columns: new[] { "EngineDisplacement", "FuelType", "OdoKilometers", "PowerInKiloWatts" },
                values: new object[] { 3.6000000000000001, 1, 27500, 213.0 });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"),
                columns: new[] { "EngineDisplacement", "FuelType", "OdoKilometers", "PowerInKiloWatts" },
                values: new object[] { 1.3999999999999999, 1, 19800, 103.0 });
        }
    }
}
