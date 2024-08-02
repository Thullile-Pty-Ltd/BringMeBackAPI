using Microsoft.OpenApi.Models;
using BringMeBack.Data;
using Microsoft.EntityFrameworkCore;
using BringMeBackAPI.Services.Users.Interfaces;
using BringMeBackAPI.Services.Reports.Services;
using BringMeBackAPI.Services.Reports.Interfaces;
using BringMeBackAPI.Services.Verifications.Interfaces;
using BringMeBackAPI.Services.Verifications.Services;
using BringMeBackAPI.Services.Notifications.Services;
using BringMeBackAPI.Services.Notifications.Interfaces;

using System.Globalization;
using System.Text.Json.Serialization;
using BringMeBackAPI.Services.Reports.Dashboards;
using BringMeBackAPI.Repository.Reports.Interfaces;
using BringMeBackAPI.Services.Reports.Dashboards.Interfaces;
using BringMeBackAPI.Repository.Reports;
using BringMeBackAPI.Repository.Users;
using BringMeBackAPI.Models.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BringMeBackAPI.Services.FileUpload;
using BringMeBackAPI.Services.Reports.Person.Services;
using BringMeBackAPI.Services.Reports.Person;
using BringMeBackAPI.Services.Reports.Animal.Service;
using BringMeBackAPI.Services.Reports.Animal;
using BringMeBackAPI.Repository.Reports.Animal;
using BringMeBackAPI.Repository.Reports.Animal.Repository;
using BringMeBackAPI.Repository.Reports.Vehicle.Repository;
using BringMeBackAPI.Repository.Reports.Vehicle;
using BringMeBackAPI.Services.Reports.Vehicle.Service;
using BringMeBackAPI.Services.Reports.Vehicle;
using BringMeBackAPI.Repository.Reports.Item.Repository;
using BringMeBackAPI.Repository.Reports.Item;
using BringMeBackAPI.Services.Reports.Item.Service;
using BringMeBackAPI.Services.Reports.Item;
using BringMeBackAPI.Services.Reports.Other;
using BringMeBackAPI.Repository.Reports.Other;
using BringMeBackAPI.Services.Reports.Other.Service;
using BringMeBackAPI.Repository.Reports.Other.Repository;
using BringMeBackAPI.Repository.Reports.Person;
using BringMeBackAPI.Repository.Reports.Person.Repository;

var builder = WebApplication.CreateBuilder(args);

// Configure globalization settings to use invariant culture
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(CultureInfo.InvariantCulture);
});

// Add AutoMapper to the service container
builder.Services.AddAutoMapper(typeof(MappingProfile)); // Register AutoMapper with the profile

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});



// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Register the Swagger generator, defining 1 or more Swagger documents
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BringMeBack API", Version = "v1" });
});

// User services
builder.Services.AddScoped<IUserService, UserService>();

//BaseReport Services
builder.Services.AddScoped<IReportService, ReportService>();

// Register your repository
builder.Services.AddScoped<IMissingPersonReportRepository, MissingPersonReportRepository>();
builder.Services.AddScoped<IFoundPersonReportRepository, FoundPersonReportRepository>();
builder.Services.AddScoped<IMissingItemReportRepository, MissingItemReportRepository>();
builder.Services.AddScoped<IFoundItemReportRepository, FoundItemReportRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Add NEW dependency injection for repositories
builder.Services.AddScoped<IChildReportRepository, ChildReportRepository>();
builder.Services.AddScoped<IAdultReportRepository, AdultReportRepository>();
builder.Services.AddScoped< ISeniorCitizenReportRepository, SeniorCitizenReportRepository>();
builder.Services.AddScoped<IWantedPersonReportRepository, WantedPersonReportRepository>();

// Add NEW services for dependency injection services
builder.Services.AddScoped<IChildReportService, ChildReportService>();
builder.Services.AddScoped<IAdultReportService, AdultReportService>();
builder.Services.AddScoped<ISeniorCitizenReportService, SeniorCitizenReportService>();
builder.Services.AddScoped<IWantedPersonReportService, WantedPersonReportService>();

// Add NEW dependency injection for repositories
builder.Services.AddScoped<ILivestockReportRepository, LivestockReportRepository>();
builder.Services.AddScoped<IPetReportRepository, PetReportRepository>();
builder.Services.AddScoped<IWildReportRepository, WildReportRepository>();

// Add NEW dependency injection for services
builder.Services.AddScoped<ILivestockReportService, LivestockReportService>();
builder.Services.AddScoped<IPetReportService, PetReportService>();
builder.Services.AddScoped<IWildReportService, WildReportService>();

// Add NEW dependency injection for repositories
builder.Services.AddScoped<IBikeReportRepository, BikeReportRepository>();
builder.Services.AddScoped<IBusReportRepository, BusReportRepository>();
builder.Services.AddScoped<ICarReportRepository, CarReportRepository>();
builder.Services.AddScoped<IHeavyDutyMachineryReportRepository, HeavyDutyMachineryReportRepository>();
builder.Services.AddScoped<ITruckReportRepository, TruckReportRepository>();

// Add NEW dependency injection for services
builder.Services.AddScoped<IBikeReportService, BikeReportService>();
builder.Services.AddScoped<IBusReportService, BusReportService>();
builder.Services.AddScoped<ICarReportService, CarReportService>();
builder.Services.AddScoped<IHeavyDutyMachineryReportService, HeavyDutyMachineryReportService>();
builder.Services.AddScoped<ITruckReportService, TruckReportService>();

// Add NEW dependency injection for repositories
builder.Services.AddScoped<IClothingAndAccessoriesRepository, ClothingAndAccessoriesRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
builder.Services.AddScoped<IHomeAndOfficeRepository, HomeAndOfficeRepository>();

// Add NEW dependency injection for services
builder.Services.AddScoped<IClothingAndAccessoriesService, ClothingAndAccessoriesService>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddScoped<IHomeAndOfficeService, HomeAndOfficeService>();

// Add NEW dependency injection for repositories
builder.Services.AddScoped<IAnnouncementReportRepository, AnnouncementReportRepository>();
builder.Services.AddScoped<IDangerZoneReportRepository, DangerZoneReportRepository>();

// Add NEW dependency injection for services
builder.Services.AddScoped<IAnnouncementReportService, AnnouncementReportService>();
builder.Services.AddScoped<IDangerZoneReportService, DangerZoneReportService>();

// Register services
builder.Services.AddScoped<IMissingPersonReportService, MissingPersonReportService>();
builder.Services.AddScoped<IMissingItemReportService, MissingItemReportService>();
builder.Services.AddScoped<IFoundPersonReportService, FoundPersonReportService>();
builder.Services.AddScoped<IFoundItemReportService, FoundItemReportService>();
builder.Services.AddScoped<IReportMatchingService, ReportMatchingService>();

builder.Services.AddScoped<IVerificationService, VerificationService>();
builder.Services.AddScoped<IOTPService, OTPService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// File Upload
builder.Services.AddScoped<IFileStorageService, FileStorageService>();

// JWT settings from appsettings.json
var jwtSettings = builder.Configuration.GetSection("Jwt");

if (string.IsNullOrEmpty(jwtSettings["Issuer"]) ||
    string.IsNullOrEmpty(jwtSettings["Audience"]) ||
    string.IsNullOrEmpty(jwtSettings["Key"]))
{
    throw new InvalidOperationException("JWT configuration values are missing.");
}

// Add JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
    };
});



var app = builder.Build();

// Configure the HTTP / Local request pipeline.
if (app.Environment.IsDevelopment())
{app.UseDeveloperExceptionPage();
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BringMeBack API v1");
        c.RoutePrefix = string.Empty; 
        c.DefaultModelsExpandDepth(-1); // Hides the schemas at the bottom of the page
      });
}

app.UseHttpsRedirection();

app.UseStaticFiles(); // Ensure this line is present to serve static files
app.UseCors(); // Applies CORS policies globally

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
