using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentDrive.Data.Migrations
{
    /// <inheritdoc />
    public partial class VehicleRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "User profile creation time (UTC).");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompanyEmployee",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Is the user employee of the company.");

            migrationBuilder.CreateTable(
                name: "VehicleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Name of the vehicle type.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleTypeClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleTypeId = table.Column<int>(type: "int", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Name of the vehicle class."),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true, comment: "Description of the vehicle class.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypeClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleTypeClasses_VehicleTypes_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalTable: "VehicleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "The owner's Id of the vehicle. Null if the vehicle is company-owned."),
                    VehicleTypeId = table.Column<int>(type: "int", nullable: false),
                    VehicleTypeCategoryId = table.Column<int>(type: "int", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Manufacturer of the vehicle."),
                    Model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Model of the vehicle."),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Vehicle's color."),
                    PricePerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Price per hour for renting the vehicle."),
                    DateOfProduction = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Vehicle's manufactured date."),
                    CurbWeightInKg = table.Column<double>(type: "float", nullable: false, comment: "Weight of the vehicle when empty in kilograms."),
                    OdoKilometers = table.Column<int>(type: "int", nullable: false, comment: "Vehicle's total kilometers traveled."),
                    EngineDisplacement = table.Column<double>(type: "float", nullable: false, comment: "Vehicle's engine capacity in liters."),
                    FuelType = table.Column<int>(type: "int", nullable: false, comment: "Vehicle's fuel type."),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true, comment: "Optional description of the vehicle."),
                    PowerInKiloWatts = table.Column<double>(type: "float", nullable: false, comment: "Power output of the vehicle's engine in kilowatts.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_VehicleTypeClasses_VehicleTypeCategoryId",
                        column: x => x.VehicleTypeCategoryId,
                        principalTable: "VehicleTypeClasses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vehicles_VehicleTypes_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalTable: "VehicleTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "VehicleTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Car" },
                    { 2, "Truck" },
                    { 3, "Motorcycle" },
                    { 4, "Bicycle" },
                    { 5, "Electric Scooter" },
                    { 6, "All Terrain Vehicle (ATV)" },
                    { 7, "Camper Trailer" },
                    { 8, "Recreational Vehicle" },
                    { 9, "Limousine" }
                });

            migrationBuilder.InsertData(
                table: "VehicleTypeClasses",
                columns: new[] { "Id", "CategoryName", "Description", "VehicleTypeId" },
                values: new object[,]
                {
                    { 1, "SUV", "Spacious and powerful car ideal for families and off-road.", 1 },
                    { 2, "Sedan", "Comfortable passenger car suitable for everyday use.", 1 },
                    { 3, "Hatchback", "Compact car with a rear door that swings upward.", 1 },
                    { 4, "Pickup", "Truck with an open cargo area in the back.", 2 },
                    { 5, "Box Truck", "Truck with a large, enclosed cargo area.", 2 },
                    { 6, "Naked", "Very good bike for everyday riding.", 3 }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Color", "CurbWeightInKg", "DateOfProduction", "Description", "EngineDisplacement", "FuelType", "Make", "Model", "OdoKilometers", "OwnerId", "PowerInKiloWatts", "PricePerHour", "VehicleTypeCategoryId", "VehicleTypeId" },
                values: new object[,]
                {
                    { new Guid("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"), "White", 1470.0, new DateTime(2021, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comfortable midsize sedan, ideal for long drives.", 2.5, 1, "Toyota", "Camry", 34500, new Guid("b90b7c45-b16f-48ef-9c01-fb3032a038ae"), 150.0, 12.50m, 2, 1 },
                    { new Guid("fe15cde2-1a90-46d4-89f1-10fda7f11743"), "Dark Green", 2045.0, new DateTime(2022, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Spacious and off-road capable SUV.", 3.6000000000000001, 1, "Jeep", "Grand Cherokee", 27500, new Guid("cd327e1f-6c3b-4e6d-a5f7-fd9e4b84357d"), 213.0, 22.00m, 1, 1 },
                    { new Guid("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"), "Silver", 1300.0, new DateTime(2021, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Compact hatchback with great fuel economy.", 1.3999999999999999, 1, "Volkswagen", "Golf", 19800, new Guid("c8fbe2fc-c8e2-4b3e-aabc-c87aebf09a8d"), 103.0, 10.00m, 3, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleTypeCategoryId",
                table: "Vehicles",
                column: "VehicleTypeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleTypeId",
                table: "Vehicles",
                column: "VehicleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTypeClasses_VehicleTypeId",
                table: "VehicleTypeClasses",
                column: "VehicleTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "VehicleTypeClasses");

            migrationBuilder.DropTable(
                name: "VehicleTypes");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsCompanyEmployee",
                table: "AspNetUsers");
        }
    }
}
