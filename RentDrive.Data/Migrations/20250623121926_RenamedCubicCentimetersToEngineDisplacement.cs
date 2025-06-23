using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentDrive.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenamedCubicCentimetersToEngineDisplacement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "VehicleTypeProperties",
                keyColumn: "Id",
                keyValue: new Guid("671fbc1d-53ef-4862-9f73-974b94ac0d25"),
                column: "Name",
                value: "Engine Displacement");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "VehicleTypeProperties",
                keyColumn: "Id",
                keyValue: new Guid("671fbc1d-53ef-4862-9f73-974b94ac0d25"),
                column: "Name",
                value: "Cubic Centimeters");
        }
    }
}
