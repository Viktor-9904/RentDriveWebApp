using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentDrive.Data.Migrations
{
    /// <inheritdoc />
    public partial class initialPostgreDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "User profile creation time (UTC)."),
                    UserType = table.Column<int>(type: "integer", nullable: false, defaultValue: 0, comment: "User's type."),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, comment: "Name of the vehicle type."),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false, comment: "Is vehicle type soft deleted")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric(18,2)", nullable: false, comment: "Available balance in wallet.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleTypeCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VehicleTypeId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, comment: "Name of the vehicle class."),
                    Description = table.Column<string>(type: "character varying(1500)", maxLength: 1500, nullable: false, comment: "Description of the vehicle class."),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false, comment: "Is vehicle type category soft deleted.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypeCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleTypeCategories_VehicleTypes_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalTable: "VehicleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleTypeProperties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleTypeId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, comment: "Property Name"),
                    ValueType = table.Column<int>(type: "integer", nullable: false, defaultValue: 0, comment: "Property value type"),
                    UnitOfMeasurement = table.Column<int>(type: "integer", nullable: false, defaultValue: 0, comment: "Property unit of measurement")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypeProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleTypeProperties_VehicleTypes_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalTable: "VehicleTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WalletTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WalletId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false, comment: "Amount of money"),
                    Type = table.Column<int>(type: "integer", nullable: false, comment: "Type of transaction - Deposit or Withdraw."),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Creation date of transaction.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalletTransactions_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false, comment: "Indicates whether the vehicle is soft deleted."),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: true, comment: "The owner's Id of the vehicle. Null if the vehicle is company-owned."),
                    VehicleTypeId = table.Column<int>(type: "integer", nullable: false),
                    VehicleTypeCategoryId = table.Column<int>(type: "integer", nullable: false),
                    Make = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "Manufacturer of the vehicle."),
                    Model = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "Model of the vehicle."),
                    Color = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, comment: "Vehicle's color."),
                    PricePerDay = table.Column<decimal>(type: "numeric(18,2)", nullable: false, comment: "Price per day for renting the vehicle."),
                    DateOfProduction = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Vehicle's manufactured date."),
                    DateAdded = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CurbWeightInKg = table.Column<double>(type: "double precision", nullable: false, comment: "Weight of the vehicle when empty in kilograms."),
                    Description = table.Column<string>(type: "character varying(1500)", maxLength: 1500, nullable: false, comment: "Optional description of the vehicle."),
                    FuelType = table.Column<int>(type: "integer", nullable: false, defaultValue: 0, comment: "Vehicle fuel type.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vehicles_VehicleTypeCategories_VehicleTypeCategoryId",
                        column: x => x.VehicleTypeCategoryId,
                        principalTable: "VehicleTypeCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vehicles_VehicleTypes_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalTable: "VehicleTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                    RenterId = table.Column<Guid>(type: "uuid", nullable: false),
                    BookedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Date of booking."),
                    CancelledOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "Date of cancelled rental."),
                    CompletedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "Rental date of completion."),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Start day of rental."),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "End day of rental."),
                    VehiclePricePerDay = table.Column<decimal>(type: "numeric(18,2)", nullable: false, comment: "Vehicle price per day."),
                    TotalPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false, comment: "Total price for renting."),
                    Status = table.Column<int>(type: "integer", nullable: false, defaultValue: 0, comment: "Status of rental")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rentals_AspNetUsers_RenterId",
                        column: x => x.RenterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rentals_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VehicleImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageURL = table.Column<string>(type: "character varying(2083)", maxLength: 2083, nullable: false, defaultValue: "images/default-image.jpg", comment: "Vehicle Image URL.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleImages_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VehicleTypePropertyValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleTypePropertyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PropertyValue = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "Property Value")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypePropertyValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleTypePropertyValues_VehicleTypeProperties_VehicleType~",
                        column: x => x.VehicleTypePropertyId,
                        principalTable: "VehicleTypeProperties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VehicleTypePropertyValues_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VehicleReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                    RentalId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReviewerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Stars = table.Column<int>(type: "integer", nullable: false, comment: "Vehicle rating scale from 0 to 10."),
                    Comment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true, comment: "Review Comment"),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Review creation date.")
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
                table: "VehicleTypeCategories",
                columns: new[] { "Id", "Description", "Name", "VehicleTypeId" },
                values: new object[,]
                {
                    { 1, "Spacious and powerful car ideal for families and off-road.", "SUV", 1 },
                    { 2, "Comfortable passenger car suitable for everyday use.", "Sedan", 1 },
                    { 3, "Compact car with a rear door that swings upward.", "Hatchback", 1 },
                    { 4, "Truck with an open cargo area in the back.", "Pickup", 2 },
                    { 5, "Truck with a large, enclosed cargo area.", "Box Truck", 2 },
                    { 6, "Very good bike for everyday riding.", "Naked", 3 }
                });

            migrationBuilder.InsertData(
                table: "VehicleTypeProperties",
                columns: new[] { "Id", "Name", "ValueType", "VehicleTypeId" },
                values: new object[] { new Guid("13aaad5c-624c-4361-a863-6ff301f5c63d"), "Door Count", 1, 1 });

            migrationBuilder.InsertData(
                table: "VehicleTypeProperties",
                columns: new[] { "Id", "Name", "UnitOfMeasurement", "ValueType", "VehicleTypeId" },
                values: new object[,]
                {
                    { new Guid("671fbc1d-53ef-4862-9f73-974b94ac0d25"), "Engine Displacement", 5, 1, 1 },
                    { new Guid("6b197695-891b-492a-8935-a54a500742c2"), "Power in KiloWatts", 8, 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "VehicleTypeProperties",
                columns: new[] { "Id", "Name", "ValueType", "VehicleTypeId" },
                values: new object[] { new Guid("77ab0d68-a40a-413a-9b4a-aa30716609db"), "Seat Count", 1, 1 });

            migrationBuilder.InsertData(
                table: "VehicleTypeProperties",
                columns: new[] { "Id", "Name", "UnitOfMeasurement", "ValueType", "VehicleTypeId" },
                values: new object[,]
                {
                    { new Guid("b924497e-006a-4d20-899f-66fde6c94ec8"), "Engine Displacement", 5, 1, 3 },
                    { new Guid("dd348516-a1dc-4336-b6c8-fb885b7afd0f"), "Power in KiloWatts", 8, 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Color", "CurbWeightInKg", "DateAdded", "DateOfProduction", "Description", "FuelType", "Make", "Model", "OwnerId", "PricePerDay", "VehicleTypeCategoryId", "VehicleTypeId" },
                values: new object[,]
                {
                    { new Guid("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"), "White", 1470.0, new DateTime(2022, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comfortable midsize sedan, ideal for long drives.", 1, "Toyota", "Camry", null, 32.50m, 2, 1 },
                    { new Guid("fe15cde2-1a90-46d4-89f1-10fda7f11743"), "Dark Green", 2045.0, new DateTime(2023, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Spacious and off-road capable SUV.", 2, "Jeep", "Grand Cherokee", null, 62.00m, 1, 1 },
                    { new Guid("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"), "Silver", 1300.0, new DateTime(2024, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Compact hatchback with great fuel economy.", 1, "Volkswagen", "Golf", null, 54.00m, 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "VehicleImages",
                columns: new[] { "Id", "ImageURL", "VehicleId" },
                values: new object[,]
                {
                    { new Guid("1d989d71-afdb-4741-8851-dedc44ffe964"), "images/vehicles/Vehicle-2.jpg", new Guid("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4") },
                    { new Guid("47725763-dc3a-485b-9f12-26df29497dd1"), "images/vehicles/Vehicle-3.jpg", new Guid("fe15cde2-1a90-46d4-89f1-10fda7f11743") },
                    { new Guid("a35616f3-67b2-4d66-95cd-78fae80883fa"), "images/vehicles/Vehicle-1.png", new Guid("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de") }
                });

            migrationBuilder.InsertData(
                table: "VehicleTypePropertyValues",
                columns: new[] { "Id", "PropertyValue", "VehicleId", "VehicleTypePropertyId" },
                values: new object[,]
                {
                    { new Guid("226d11c2-6ace-482d-8c05-052ae6a36df5"), "5", new Guid("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"), new Guid("13aaad5c-624c-4361-a863-6ff301f5c63d") },
                    { new Guid("43049eae-e00f-48c0-94a1-dbcd66f1b892"), "218", new Guid("fe15cde2-1a90-46d4-89f1-10fda7f11743"), new Guid("dd348516-a1dc-4336-b6c8-fb885b7afd0f") },
                    { new Guid("4bdc05a2-559f-412c-994a-69c1ac6ab24f"), "2487", new Guid("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"), new Guid("671fbc1d-53ef-4862-9f73-974b94ac0d25") },
                    { new Guid("53c60eee-041b-4cf7-828f-43c0c3000305"), "4", new Guid("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"), new Guid("13aaad5c-624c-4361-a863-6ff301f5c63d") },
                    { new Guid("838bb66e-a194-4b05-a35d-397746dbc375"), "7", new Guid("fe15cde2-1a90-46d4-89f1-10fda7f11743"), new Guid("77ab0d68-a40a-413a-9b4a-aa30716609db") },
                    { new Guid("92820a2c-2324-4279-aff9-8e606f868200"), "3604", new Guid("fe15cde2-1a90-46d4-89f1-10fda7f11743"), new Guid("671fbc1d-53ef-4862-9f73-974b94ac0d25") },
                    { new Guid("c9951dc1-d840-42b4-854e-44104883613f"), "5", new Guid("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"), new Guid("77ab0d68-a40a-413a-9b4a-aa30716609db") },
                    { new Guid("deb2022f-58a6-46de-93de-78266eb453b2"), "110", new Guid("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"), new Guid("dd348516-a1dc-4336-b6c8-fb885b7afd0f") },
                    { new Guid("dfd548e3-0542-435e-b80e-b349a0e86c79"), "1395", new Guid("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"), new Guid("671fbc1d-53ef-4862-9f73-974b94ac0d25") },
                    { new Guid("e0c89c2c-28ad-496d-bee9-17da515dee11"), "151", new Guid("6a8e2d12-04a3-4c55-8b2b-f9a0f1fd35de"), new Guid("dd348516-a1dc-4336-b6c8-fb885b7afd0f") },
                    { new Guid("e400d83c-9533-426f-b6ce-5916ce24f510"), "5", new Guid("ff71fcbc-6829-47fd-81c7-d16d7c2c34b4"), new Guid("77ab0d68-a40a-413a-9b4a-aa30716609db") },
                    { new Guid("ea03bee5-44ac-4140-90e4-5932694920b7"), "4", new Guid("fe15cde2-1a90-46d4-89f1-10fda7f11743"), new Guid("13aaad5c-624c-4361-a863-6ff301f5c63d") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_RenterId",
                table: "Rentals",
                column: "RenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_VehicleId",
                table: "Rentals",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleImages_VehicleId",
                table: "VehicleImages",
                column: "VehicleId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_OwnerId",
                table: "Vehicles",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleTypeCategoryId",
                table: "Vehicles",
                column: "VehicleTypeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleTypeId",
                table: "Vehicles",
                column: "VehicleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTypeCategories_VehicleTypeId_Name",
                table: "VehicleTypeCategories",
                columns: new[] { "VehicleTypeId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTypeProperties_VehicleTypeId",
                table: "VehicleTypeProperties",
                column: "VehicleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTypePropertyValues_VehicleId_VehicleTypePropertyId",
                table: "VehicleTypePropertyValues",
                columns: new[] { "VehicleId", "VehicleTypePropertyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTypePropertyValues_VehicleTypePropertyId",
                table: "VehicleTypePropertyValues",
                column: "VehicleTypePropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UserId",
                table: "Wallets",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransactions_WalletId",
                table: "WalletTransactions",
                column: "WalletId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "VehicleImages");

            migrationBuilder.DropTable(
                name: "VehicleReviews");

            migrationBuilder.DropTable(
                name: "VehicleTypePropertyValues");

            migrationBuilder.DropTable(
                name: "WalletTransactions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "VehicleTypeProperties");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "VehicleTypeCategories");

            migrationBuilder.DropTable(
                name: "VehicleTypes");
        }
    }
}
