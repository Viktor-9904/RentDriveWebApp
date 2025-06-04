using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentDrive.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateVehicleOwnerIdsToNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"),
                column: "OwnerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("fe15cde2-1a90-46d4-89f1-10fda7f11743"),
                column: "OwnerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"),
                column: "OwnerId",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"),
                column: "OwnerId",
                value: new Guid("b90b7c45-b16f-48ef-9c01-fb3032a038ae"));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("fe15cde2-1a90-46d4-89f1-10fda7f11743"),
                column: "OwnerId",
                value: new Guid("cd327e1f-6c3b-4e6d-a5f7-fd9e4b84357d"));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"),
                column: "OwnerId",
                value: new Guid("c8fbe2fc-c8e2-4b3e-aabc-c87aebf09a8d"));
        }
    }
}
