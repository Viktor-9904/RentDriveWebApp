using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentDrive.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserWalletSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "UserId" },
                values: new object[,]
                {
                    { new Guid("3286205e-24a5-47f7-a418-141ce2f61a7f"), 620.97m, new Guid("a8b2e9f4-927d-4f87-a457-bf95cd4526dc") },
                    { new Guid("d6b9cab2-c4f8-416c-8dbc-63bab5c5e860"), 420.32m, new Guid("e7df3bc2-1c20-4895-b8c9-781ad6cf892a") },
                    { new Guid("fa010230-0d91-466b-a984-d47cd7651002"), 4120.19m, new Guid("d56b4e71-7c38-4c3f-8c85-ff2b7cfd2f01") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("3286205e-24a5-47f7-a418-141ce2f61a7f"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("d6b9cab2-c4f8-416c-8dbc-63bab5c5e860"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("fa010230-0d91-466b-a984-d47cd7651002"));
        }
    }
}
