using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentDrive.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedVehicleReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VehicleReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RentalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false, comment: "Vehicle rating scale from 0 to 10."),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Review Comment"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Review creation date.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleReviews_AspNetUsers_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VehicleReviews_Rentals_RentalId",
                        column: x => x.RentalId,
                        principalTable: "Rentals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VehicleReviews_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReviews_RentalId",
                table: "VehicleReviews",
                column: "RentalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReviews_ReviewerId",
                table: "VehicleReviews",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleReviews_VehicleId",
                table: "VehicleReviews",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleReviews");
        }
    }
}
