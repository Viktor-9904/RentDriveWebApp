using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentDrive.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedVehicleReviewSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "VehicleReviews",
                columns: new[] { "Id", "Comment", "CreatedOn", "RentalId", "ReviewerId", "Stars", "VehicleId" },
                values: new object[,]
                {
                    { new Guid("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Smooth ride, very clean, great experience!", new DateTime(2025, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a1a25e78-2b30-4f77-a899-08db1682a00a"), new Guid("d56b4e71-7c38-4c3f-8c85-ff2b7cfd2f01"), 5, new Guid("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de") },
                    { new Guid("22222222-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Engine was louder than expected, not well maintained.", new DateTime(2025, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9cfe3db6-50e1-41e4-8a98-3cdba63c20b1"), new Guid("e7df3bc2-1c20-4895-b8c9-781ad6cf892a"), 2, new Guid("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de") },
                    { new Guid("33333333-cccc-cccc-cccc-cccccccccccc"), "Powerful SUV, great for the mountains.", new DateTime(2025, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b2c98c1a-45f4-4b89-9a74-51cfa684b9e2"), new Guid("d56b4e71-7c38-4c3f-8c85-ff2b7cfd2f01"), 4, new Guid("fe15cde2-1a90-46d4-89f1-10fda7f11743") },
                    { new Guid("44444444-dddd-dddd-dddd-dddddddddddd"), "Uncomfortable seats and poor air conditioning.", new DateTime(2025, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a5f61b5a-883e-47f4-8189-44b197967d5f"), new Guid("e7df3bc2-1c20-4895-b8c9-781ad6cf892a"), 1, new Guid("fe15cde2-1a90-46d4-89f1-10fda7f11743") },
                    { new Guid("55555555-eeee-eeee-eeee-eeeeeeeeeeee"), "Decent for city driving, but not spacious.", new DateTime(2025, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("dca1c233-b01b-4f6c-a0fc-f6b709bd92ef"), new Guid("e7df3bc2-1c20-4895-b8c9-781ad6cf892a"), 3, new Guid("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4") },
                    { new Guid("66666666-ffff-ffff-ffff-ffffffffffff"), "Very fuel efficient and easy to park!", new DateTime(2025, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c63cc240-39c6-4d55-b5cf-bd17912825fc"), new Guid("d56b4e71-7c38-4c3f-8c85-ff2b7cfd2f01"), 5, new Guid("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "VehicleReviews",
                keyColumn: "Id",
                keyValue: new Guid("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "VehicleReviews",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "VehicleReviews",
                keyColumn: "Id",
                keyValue: new Guid("33333333-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "VehicleReviews",
                keyColumn: "Id",
                keyValue: new Guid("44444444-dddd-dddd-dddd-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "VehicleReviews",
                keyColumn: "Id",
                keyValue: new Guid("55555555-eeee-eeee-eeee-eeeeeeeeeeee"));

            migrationBuilder.DeleteData(
                table: "VehicleReviews",
                keyColumn: "Id",
                keyValue: new Guid("66666666-ffff-ffff-ffff-ffffffffffff"));
        }
    }
}
