using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentDrive.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedRentalSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Rentals",
                columns: new[] { "Id", "BookedOn", "CancelledOn", "CompletedOn", "EndDate", "RenterId", "StartDate", "TotalPrice", "VehicleId" },
                values: new object[,]
                {
                    { new Guid("7bb21c7b-d0d8-4de0-8d84-2e8d4bffec85"), new DateTime(2025, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e7df3bc2-1c20-4895-b8c9-781ad6cf892a"), new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 162.50m, new Guid("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de") },
                    { new Guid("9cfe3db6-50e1-41e4-8a98-3cdba63c20b1"), new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e7df3bc2-1c20-4895-b8c9-781ad6cf892a"), new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 162.50m, new Guid("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de") }
                });

            migrationBuilder.InsertData(
                table: "Rentals",
                columns: new[] { "Id", "BookedOn", "CancelledOn", "CompletedOn", "EndDate", "RenterId", "StartDate", "Status", "TotalPrice", "VehicleId" },
                values: new object[] { new Guid("a1a25e78-2b30-4f77-a899-08db1682a00a"), new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d56b4e71-7c38-4c3f-8c85-ff2b7cfd2f01"), new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 162.50m, new Guid("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de") });

            migrationBuilder.InsertData(
                table: "Rentals",
                columns: new[] { "Id", "BookedOn", "CancelledOn", "CompletedOn", "EndDate", "RenterId", "StartDate", "TotalPrice", "VehicleId" },
                values: new object[] { new Guid("a5f61b5a-883e-47f4-8189-44b197967d5f"), new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e7df3bc2-1c20-4895-b8c9-781ad6cf892a"), new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 372.00m, new Guid("fe15cde2-1a90-46d4-89f1-10fda7f11743") });

            migrationBuilder.InsertData(
                table: "Rentals",
                columns: new[] { "Id", "BookedOn", "CancelledOn", "CompletedOn", "EndDate", "RenterId", "StartDate", "Status", "TotalPrice", "VehicleId" },
                values: new object[] { new Guid("b2c98c1a-45f4-4b89-9a74-51cfa684b9e2"), new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d56b4e71-7c38-4c3f-8c85-ff2b7cfd2f01"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 434.00m, new Guid("fe15cde2-1a90-46d4-89f1-10fda7f11743") });

            migrationBuilder.InsertData(
                table: "Rentals",
                columns: new[] { "Id", "BookedOn", "CancelledOn", "CompletedOn", "EndDate", "RenterId", "StartDate", "TotalPrice", "VehicleId" },
                values: new object[] { new Guid("c63cc240-39c6-4d55-b5cf-bd17912825fc"), new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d56b4e71-7c38-4c3f-8c85-ff2b7cfd2f01"), new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 270.00m, new Guid("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4") });

            migrationBuilder.InsertData(
                table: "Rentals",
                columns: new[] { "Id", "BookedOn", "CancelledOn", "CompletedOn", "EndDate", "RenterId", "StartDate", "Status", "TotalPrice", "VehicleId" },
                values: new object[] { new Guid("dca1c233-b01b-4f6c-a0fc-f6b709bd92ef"), new DateTime(2025, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e7df3bc2-1c20-4895-b8c9-781ad6cf892a"), new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 270.00m, new Guid("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4") });

            migrationBuilder.InsertData(
                table: "Rentals",
                columns: new[] { "Id", "BookedOn", "CancelledOn", "CompletedOn", "EndDate", "RenterId", "StartDate", "TotalPrice", "VehicleId" },
                values: new object[,]
                {
                    { new Guid("f471e13c-5fae-4421-9fa4-300b08b97c28"), new DateTime(2025, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d56b4e71-7c38-4c3f-8c85-ff2b7cfd2f01"), new DateTime(2025, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 270.00m, new Guid("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4") },
                    { new Guid("f62e7790-609b-4a42-a01c-fbb6d8c88f89"), new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d56b4e71-7c38-4c3f-8c85-ff2b7cfd2f01"), new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 372.00m, new Guid("fe15cde2-1a90-46d4-89f1-10fda7f11743") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: new Guid("7bb21c7b-d0d8-4de0-8d84-2e8d4bffec85"));

            migrationBuilder.DeleteData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: new Guid("9cfe3db6-50e1-41e4-8a98-3cdba63c20b1"));

            migrationBuilder.DeleteData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: new Guid("a1a25e78-2b30-4f77-a899-08db1682a00a"));

            migrationBuilder.DeleteData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: new Guid("a5f61b5a-883e-47f4-8189-44b197967d5f"));

            migrationBuilder.DeleteData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: new Guid("b2c98c1a-45f4-4b89-9a74-51cfa684b9e2"));

            migrationBuilder.DeleteData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: new Guid("c63cc240-39c6-4d55-b5cf-bd17912825fc"));

            migrationBuilder.DeleteData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: new Guid("dca1c233-b01b-4f6c-a0fc-f6b709bd92ef"));

            migrationBuilder.DeleteData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: new Guid("f471e13c-5fae-4421-9fa4-300b08b97c28"));

            migrationBuilder.DeleteData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: new Guid("f62e7790-609b-4a42-a01c-fbb6d8c88f89"));
        }
    }
}
