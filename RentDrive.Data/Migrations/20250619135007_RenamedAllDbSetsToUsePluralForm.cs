using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentDrive.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenamedAllDbSetsToUsePluralForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleTypeCategory_VehicleTypeCategoryId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTypeCategory_VehicleTypes_VehicleTypeId",
                table: "VehicleTypeCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTypeProperty_VehicleTypes_VehicleTypeId",
                table: "VehicleTypeProperty");

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
                name: "PK_VehicleTypeProperty",
                table: "VehicleTypeProperty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleTypeCategory",
                table: "VehicleTypeCategory");

            migrationBuilder.RenameTable(
                name: "VehicleTypePropertyValue",
                newName: "VehicleTypePropertyValues");

            migrationBuilder.RenameTable(
                name: "VehicleTypeProperty",
                newName: "VehicleTypeProperties");

            migrationBuilder.RenameTable(
                name: "VehicleTypeCategory",
                newName: "VehicleTypeCategories");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleTypePropertyValue_VehicleTypePropertyId",
                table: "VehicleTypePropertyValues",
                newName: "IX_VehicleTypePropertyValues_VehicleTypePropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleTypePropertyValue_VehicleId_VehicleTypePropertyId",
                table: "VehicleTypePropertyValues",
                newName: "IX_VehicleTypePropertyValues_VehicleId_VehicleTypePropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleTypeProperty_VehicleTypeId",
                table: "VehicleTypeProperties",
                newName: "IX_VehicleTypeProperties_VehicleTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleTypeCategory_VehicleTypeId",
                table: "VehicleTypeCategories",
                newName: "IX_VehicleTypeCategories_VehicleTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleTypePropertyValues",
                table: "VehicleTypePropertyValues",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleTypeProperties",
                table: "VehicleTypeProperties",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleTypeCategories",
                table: "VehicleTypeCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleTypeCategories_VehicleTypeCategoryId",
                table: "Vehicles",
                column: "VehicleTypeCategoryId",
                principalTable: "VehicleTypeCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleTypeCategories_VehicleTypes_VehicleTypeId",
                table: "VehicleTypeCategories",
                column: "VehicleTypeId",
                principalTable: "VehicleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleTypeProperties_VehicleTypes_VehicleTypeId",
                table: "VehicleTypeProperties",
                column: "VehicleTypeId",
                principalTable: "VehicleTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleTypePropertyValues_VehicleTypeProperties_VehicleTypePropertyId",
                table: "VehicleTypePropertyValues",
                column: "VehicleTypePropertyId",
                principalTable: "VehicleTypeProperties",
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
                name: "FK_Vehicles_VehicleTypeCategories_VehicleTypeCategoryId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTypeCategories_VehicleTypes_VehicleTypeId",
                table: "VehicleTypeCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTypeProperties_VehicleTypes_VehicleTypeId",
                table: "VehicleTypeProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTypePropertyValues_VehicleTypeProperties_VehicleTypePropertyId",
                table: "VehicleTypePropertyValues");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTypePropertyValues_Vehicles_VehicleId",
                table: "VehicleTypePropertyValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleTypePropertyValues",
                table: "VehicleTypePropertyValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleTypeProperties",
                table: "VehicleTypeProperties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleTypeCategories",
                table: "VehicleTypeCategories");

            migrationBuilder.RenameTable(
                name: "VehicleTypePropertyValues",
                newName: "VehicleTypePropertyValue");

            migrationBuilder.RenameTable(
                name: "VehicleTypeProperties",
                newName: "VehicleTypeProperty");

            migrationBuilder.RenameTable(
                name: "VehicleTypeCategories",
                newName: "VehicleTypeCategory");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleTypePropertyValues_VehicleTypePropertyId",
                table: "VehicleTypePropertyValue",
                newName: "IX_VehicleTypePropertyValue_VehicleTypePropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleTypePropertyValues_VehicleId_VehicleTypePropertyId",
                table: "VehicleTypePropertyValue",
                newName: "IX_VehicleTypePropertyValue_VehicleId_VehicleTypePropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleTypeProperties_VehicleTypeId",
                table: "VehicleTypeProperty",
                newName: "IX_VehicleTypeProperty_VehicleTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleTypeCategories_VehicleTypeId",
                table: "VehicleTypeCategory",
                newName: "IX_VehicleTypeCategory_VehicleTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleTypePropertyValue",
                table: "VehicleTypePropertyValue",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleTypeProperty",
                table: "VehicleTypeProperty",
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
                name: "FK_VehicleTypeProperty_VehicleTypes_VehicleTypeId",
                table: "VehicleTypeProperty",
                column: "VehicleTypeId",
                principalTable: "VehicleTypes",
                principalColumn: "Id");

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
