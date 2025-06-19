using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentDrive.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenamedVehicleTypePropertyValuesToVehicleTypePropertyValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTypePropertyValues_VehicleTypeProperty_VehicleTypePropertyId",
                table: "VehicleTypePropertyValues");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTypePropertyValues_Vehicles_VehicleId",
                table: "VehicleTypePropertyValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleTypePropertyValues",
                table: "VehicleTypePropertyValues");

            migrationBuilder.RenameTable(
                name: "VehicleTypePropertyValues",
                newName: "VehicleTypePropertyValue");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleTypePropertyValues_VehicleTypePropertyId",
                table: "VehicleTypePropertyValue",
                newName: "IX_VehicleTypePropertyValue_VehicleTypePropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleTypePropertyValues_VehicleId_VehicleTypePropertyId",
                table: "VehicleTypePropertyValue",
                newName: "IX_VehicleTypePropertyValue_VehicleId_VehicleTypePropertyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleTypePropertyValue",
                table: "VehicleTypePropertyValue",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleTypePropertyValue_VehicleTypeProperty_VehicleTypePropertyId",
                table: "VehicleTypePropertyValue",
                column: "VehicleTypePropertyId",
                principalTable: "VehicleTypeProperty",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleTypePropertyValue_Vehicles_VehicleId",
                table: "VehicleTypePropertyValue",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTypePropertyValue_VehicleTypeProperty_VehicleTypePropertyId",
                table: "VehicleTypePropertyValue");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTypePropertyValue_Vehicles_VehicleId",
                table: "VehicleTypePropertyValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleTypePropertyValue",
                table: "VehicleTypePropertyValue");

            migrationBuilder.RenameTable(
                name: "VehicleTypePropertyValue",
                newName: "VehicleTypePropertyValues");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleTypePropertyValue_VehicleTypePropertyId",
                table: "VehicleTypePropertyValues",
                newName: "IX_VehicleTypePropertyValues_VehicleTypePropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleTypePropertyValue_VehicleId_VehicleTypePropertyId",
                table: "VehicleTypePropertyValues",
                newName: "IX_VehicleTypePropertyValues_VehicleId_VehicleTypePropertyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleTypePropertyValues",
                table: "VehicleTypePropertyValues",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleTypePropertyValues_VehicleTypeProperty_VehicleTypePropertyId",
                table: "VehicleTypePropertyValues",
                column: "VehicleTypePropertyId",
                principalTable: "VehicleTypeProperty",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleTypePropertyValues_Vehicles_VehicleId",
                table: "VehicleTypePropertyValues",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id");
        }
    }
}
