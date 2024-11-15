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

//Report Services
builder.Services.AddScoped<IReportService, ReportService>();

// Register your repository
builder.Services.AddScoped<IMissingPersonReportRepository, MissingPersonReportRepository>();
builder.Services.AddScoped<IFoundPersonReportRepository, FoundPersonReportRepository>();
builder.Services.AddScoped<IMissingItemReportRepository, MissingItemReportRepository>();
builder.Services.AddScoped<IFoundItemReportRepository, FoundItemReportRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Register services
builder.Services.AddScoped<IMissingPersonReportService, MissingPersonReportService>();
builder.Services.AddScoped<IMissingItemReportService, MissingItemReportService>();
builder.Services.AddScoped<IFoundPersonReportService, FoundPersonReportService>();
builder.Services.AddScoped<IFoundItemReportService, FoundItemReportService>();
builder.Services.AddScoped<IReportMatchingService, ReportMatchingService>();

//builder.Services.AddScoped<ICommentService, CommentService>();
//builder.Services.AddScoped<IDonationService, DonationService>();
//builder.Services.AddScoped<IAssociateService, AssociateService>();
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
