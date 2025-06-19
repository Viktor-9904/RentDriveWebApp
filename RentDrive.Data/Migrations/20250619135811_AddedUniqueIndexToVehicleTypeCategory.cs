using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentDrive.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedUniqueIndexToVehicleTypeCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VehicleTypeCategories_VehicleTypeId",
                table: "VehicleTypeCategories");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "VehicleTypeCategories",
                newName: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTypeCategories_VehicleTypeId_Name",
                table: "VehicleTypeCategories",
                columns: new[] { "VehicleTypeId", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VehicleTypeCategories_VehicleTypeId_Name",
                table: "VehicleTypeCategories");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "VehicleTypeCategories",
                newName: "CategoryName");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTypeCategories_VehicleTypeId",
                table: "VehicleTypeCategories",
                column: "VehicleTypeId");
        }
    }
}
