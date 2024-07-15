using Microsoft.OpenApi.Models;
using BringMeBack.Data;
using Microsoft.EntityFrameworkCore;
using BringMeBackAPI.Services.Users.Interfaces;
using BringMeBackAPI.Services.Reports.Services;
using BringMeBackAPI.Services.Reports.Interfaces;
using BringMeBackAPI.Services.Users.Services;
using BringMeBackAPI.Services.Verifications.Interfaces;
using BringMeBackAPI.Services.Verifications.Services;
using BringMeBackAPI.Services.Notifications.Services;
using BringMeBackAPI.Services.Notifications.Interfaces;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Configure globalization settings to use invariant culture
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(CultureInfo.InvariantCulture);
});

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

// Register the Swagger generator, defining 1 or more Swagger documents
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BringMeBack API", Version = "v1" });
});

// Register services
builder.Services.AddScoped<IUserservice, UserService>();

//Report Services
builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddScoped<ICommentService, CommentService>();
//builder.Services.AddScoped<IDonationService, DonationService>();
builder.Services.AddScoped<IAssociateService, AssociateService>();
builder.Services.AddScoped<IVerificationService, VerificationService>();
builder.Services.AddScoped<IOTPService, OTPService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

var app = builder.Build();

// Configure the HTTP / Local request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BringMeBack API v1");
        c.RoutePrefix = string.Empty; 
        c.DefaultModelsExpandDepth(-1); // Hides the schemas at the bottom of the page
      });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
