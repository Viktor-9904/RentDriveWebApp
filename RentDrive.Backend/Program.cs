using Microsoft.EntityFrameworkCore;

using RentDrive.Data;

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

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
