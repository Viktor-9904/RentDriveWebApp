using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentDrive.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedVehiclePropertyTypeAndVehiclePropertyTypeValueAndRenamedVehicleTypeClassesToVehicleTypeCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleTypeClasses_VehicleTypeCategoryId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTypeClasses_VehicleTypes_VehicleTypeId",
                table: "VehicleTypeClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTypePropertyValue_VehicleTypeProperty_VehicleTypePropertyId",
                table: "VehicleTypePropertyValue");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTypePropertyValue_Vehicles_VehicleId",
                table: "VehicleTypePropertyValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleTypePropertyValue",
                table: "VehicleTypePropertyValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleTypeClasses",
                table: "VehicleTypeClasses");

            migrationBuilder.RenameTable(
                name: "VehicleTypePropertyValue",
                newName: "VehicleTypePropertyValues");

            migrationBuilder.RenameTable(
                name: "VehicleTypeClasses",
                newName: "VehicleTypeCategory");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleTypePropertyValue_VehicleTypePropertyId",
                table: "VehicleTypePropertyValues",
                newName: "IX_VehicleTypePropertyValues_VehicleTypePropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleTypePropertyValue_VehicleId_VehicleTypePropertyId",
                table: "VehicleTypePropertyValues",
                newName: "IX_VehicleTypePropertyValues_VehicleId_VehicleTypePropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleTypeClasses_VehicleTypeId",
                table: "VehicleTypeCategory",
                newName: "IX_VehicleTypeCategory_VehicleTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleTypePropertyValues",
                table: "VehicleTypePropertyValues",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleTypeCategory",
                table: "VehicleTypeCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleTypeCategory_VehicleTypeCategoryId",
                table: "Vehicles",
                column: "VehicleTypeCategoryId",
                principalTable: "VehicleTypeCategory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleTypeCategory_VehicleTypes_VehicleTypeId",
                table: "VehicleTypeCategory",
                column: "VehicleTypeId",
                principalTable: "VehicleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleTypeCategory_VehicleTypeCategoryId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTypeCategory_VehicleTypes_VehicleTypeId",
                table: "VehicleTypeCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTypePropertyValues_VehicleTypeProperty_VehicleTypePropertyId",
                table: "VehicleTypePropertyValues");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTypePropertyValues_Vehicles_VehicleId",
                table: "VehicleTypePropertyValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleTypePropertyValues",
                table: "VehicleTypePropertyValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleTypeCategory",
                table: "VehicleTypeCategory");

            migrationBuilder.RenameTable(
                name: "VehicleTypePropertyValues",
                newName: "VehicleTypePropertyValue");

            migrationBuilder.RenameTable(
                name: "VehicleTypeCategory",
                newName: "VehicleTypeClasses");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleTypePropertyValues_VehicleTypePropertyId",
                table: "VehicleTypePropertyValue",
                newName: "IX_VehicleTypePropertyValue_VehicleTypePropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleTypePropertyValues_VehicleId_VehicleTypePropertyId",
                table: "VehicleTypePropertyValue",
                newName: "IX_VehicleTypePropertyValue_VehicleId_VehicleTypePropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleTypeCategory_VehicleTypeId",
                table: "VehicleTypeClasses",
                newName: "IX_VehicleTypeClasses_VehicleTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleTypePropertyValue",
                table: "VehicleTypePropertyValue",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleTypeClasses",
                table: "VehicleTypeClasses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleTypeClasses_VehicleTypeCategoryId",
                table: "Vehicles",
                column: "VehicleTypeCategoryId",
                principalTable: "VehicleTypeClasses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleTypeClasses_VehicleTypes_VehicleTypeId",
                table: "VehicleTypeClasses",
                column: "VehicleTypeId",
                principalTable: "VehicleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
