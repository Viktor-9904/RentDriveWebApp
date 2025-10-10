using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentDrive.Data;
using RentDrive.Data.Configuration;
using RentDrive.Data.Models;
using RentDrive.Data.Repository;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data;
using RentDrive.Services.Data.Interfaces;
using System.Net;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string? dbHost = Environment.GetEnvironmentVariable("DB_HOST");
string? dbPort = Environment.GetEnvironmentVariable("DB_PORT");
string? dbName = Environment.GetEnvironmentVariable("DB_NAME");
string? dbUser = Environment.GetEnvironmentVariable("DB_USER");
string? dbPass = Environment.GetEnvironmentVariable("DB_PASS");

string connectionString = (!string.IsNullOrEmpty(dbHost) &&
                           !string.IsNullOrEmpty(dbPort) &&
                           !string.IsNullOrEmpty(dbName) &&
                           !string.IsNullOrEmpty(dbUser) &&
                           !string.IsNullOrEmpty(dbPass))
    ? $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={dbPass}"
    : builder.Configuration.GetConnectionString("DefaultConnection")!;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

string frontEndURL = builder.Configuration["FrontEndURL:URL"]!;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Add services to the container.
builder.Services.AddDbContext<RentDriveDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole<Guid>>(cfg =>
    {
        IdentityConfiguration(builder, cfg);
    })
    .AddEntityFrameworkStores<RentDriveDbContext>()
    .AddRoles<IdentityRole<Guid>>()
    .AddSignInManager<SignInManager<ApplicationUser>>()
    .AddUserManager<UserManager<ApplicationUser>>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Events = new CookieAuthenticationEvents()
        {
            OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Task.CompletedTask;
            },
            OnRedirectToAccessDenied = context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontEnd", policy =>
    {
        policy.WithOrigins(frontEndURL)
        .AllowCredentials()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddSignalR();

builder.Services.AddScoped<IRepository<ApplicationUser, Guid>, BaseRepository<ApplicationUser, Guid>>();
builder.Services.AddScoped<IRepository<Vehicle, Guid>, BaseRepository<Vehicle, Guid>>();
builder.Services.AddScoped<IRepository<VehicleType, Guid>, BaseRepository<VehicleType, Guid>>();
builder.Services.AddScoped<IRepository<VehicleTypeCategory, Guid>, BaseRepository<VehicleTypeCategory, Guid>>();
builder.Services.AddScoped<IRepository<VehicleImage, Guid>, BaseRepository<VehicleImage, Guid>>();
builder.Services.AddScoped<IRepository<ApplicationUser, Guid>, BaseRepository<ApplicationUser, Guid>>();
builder.Services.AddScoped<IRepository<VehicleTypeProperty, Guid>, BaseRepository<VehicleTypeProperty, Guid>>();
builder.Services.AddScoped<IRepository<VehicleTypePropertyValue, Guid>, BaseRepository<VehicleTypePropertyValue, Guid>>();
builder.Services.AddScoped<IRepository<VehicleType, int>, BaseRepository<VehicleType, int>>();
builder.Services.AddScoped<IRepository<VehicleTypeCategory, int>, BaseRepository<VehicleTypeCategory, int>>();
builder.Services.AddScoped<IRepository<Rental, Guid>, BaseRepository<Rental, Guid>>();
builder.Services.AddScoped<IRepository<VehicleReview, Guid>, BaseRepository<VehicleReview, Guid>>();
builder.Services.AddScoped<IRepository<Wallet, Guid>, BaseRepository<Wallet, Guid>>();
builder.Services.AddScoped<IRepository<WalletTransaction, Guid>, BaseRepository<WalletTransaction, Guid>>();
builder.Services.AddScoped<IRepository<ChatMessage, Guid>, BaseRepository<ChatMessage, Guid>>();

builder.Services.AddScoped<IVehicleImageService, VehicleImageService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVehicleTypePropertyValueService, VehicleTypePropertyValueService>();
builder.Services.AddScoped<IVehicleTypePropertyService, VehicleTypePropertyService>();
builder.Services.AddScoped<IVehicleTypeService, VehicleTypeService>();
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IVehicleTypeCategoryService, VehicleTypeCategoryService>();
builder.Services.AddScoped<IRentalService, RentalService>();
builder.Services.AddScoped<IVehicleReviewService, VehicleReviewService>();
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddScoped<IWalletTransaction, WalletTransactionService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddSingleton<IEncryptionService>(new EncryptionService(builder.Configuration["Encryption:Key"]));

WebApplication app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RentDriveDbContext>();
    db.Database.Migrate();

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    await UserSeeder.SeedUsersAsync(userManager);
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    await UserSeeder.SeedUsersAsync(userManager);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontEnd");

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chat");

app.Run();
static void IdentityConfiguration(WebApplicationBuilder builder, IdentityOptions cfg)
{
    cfg.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
    cfg.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
    cfg.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
    cfg.Password.RequireDigit = builder.Configuration.GetValue<bool>("Identity:Password:RequireDigit");
    cfg.Password.RequiredLength = builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");

    cfg.User.RequireUniqueEmail = builder.Configuration.GetValue<bool>("Identity:User:RequireUniqueEmail");

    cfg.SignIn.RequireConfirmedAccount = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
    cfg.SignIn.RequireConfirmedEmail = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedEmail");
    cfg.SignIn.RequireConfirmedPhoneNumber = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedPhoneNumber");
}