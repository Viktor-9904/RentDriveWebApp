using Microsoft.EntityFrameworkCore;

using RentDrive.Data;
using RentDrive.Data.Models;
using RentDrive.Data.Repository;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data;
using RentDrive.Services.Data.Interfaces;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
string frontEndURL = builder.Configuration["FrontEndURL:URL"]!;

// Add services to the container.
builder.Services
    .AddDbContext<RentDriveDbContext>(options =>
    {
        options.UseSqlServer(connectionString);
    });

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontEnd", policy =>
    {
        policy.WithOrigins(frontEndURL)
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddScoped<IRepository<ApplicationUser, Guid>, BaseRepository<ApplicationUser, Guid>>();
builder.Services.AddScoped<IRepository<Vehicle, Guid>, BaseRepository<Vehicle, Guid>>();
builder.Services.AddScoped<IRepository<VehicleType, Guid>, BaseRepository<VehicleType, Guid>>();
builder.Services.AddScoped<IRepository<VehicleTypeCategory, Guid>, BaseRepository<VehicleTypeCategory, Guid>>();
builder.Services.AddScoped<IRepository<VehicleImages, Guid>, BaseRepository<VehicleImages, Guid>>();

builder.Services.AddScoped<IVehicleImageService, VehicleImageService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontEnd");

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
