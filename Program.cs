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
using System.Text.Json.Serialization;
using BringMeBackAPI.Services.Reports.Dashboards;
using BringMeBackAPI.Repository.Reports.Interfaces;
using BringMeBackAPI.Services.Reports.Dashboards.Interfaces;
using BringMeBackAPI.Repository.Reports;

var builder = WebApplication.CreateBuilder(args);

// Configure globalization settings to use invariant culture
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(CultureInfo.InvariantCulture);
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
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
builder.Services.AddScoped<IUserservice, UserService>();
builder.Services.AddScoped<IOrganizationUserService, OrganizationUserService>();
builder.Services.AddScoped<ICommunityMemberService, CommunityMemberService>();
builder.Services.AddScoped<IFamilyMemberService, FamilyMemberService>();
builder.Services.AddScoped<IPublicAuthorityService, PublicAuthorityService>();
builder.Services.AddScoped<IVolunteerService, VolunteerService>();
builder.Services.AddScoped<IDonorSupporterService, DonorSupporterService>();

//Report Services
builder.Services.AddScoped<IReportService, ReportService>();

// Register your repository
builder.Services.AddScoped<IMissingPersonReportRepository, MissingPersonReportRepository>();
builder.Services.AddScoped<IFoundPersonReportRepository, FoundPersonReportRepository>();
builder.Services.AddScoped<IMissingItemReportRepository, MissingItemReportRepository>();
builder.Services.AddScoped<IFoundItemReportRepository, FoundItemReportRepository>();

// Register services
builder.Services.AddScoped<IMissingPersonReportService, MissingPersonReportService>();
builder.Services.AddScoped<IMissingItemReportService, MissingItemReportService>();
builder.Services.AddScoped<IFoundPersonReportService, FoundPersonReportService>();
builder.Services.AddScoped<IFoundItemReportService, FoundItemReportService>();

builder.Services.AddScoped<ICommentService, CommentService>();
//builder.Services.AddScoped<IDonationService, DonationService>();
builder.Services.AddScoped<IAssociateService, AssociateService>();
builder.Services.AddScoped<IVerificationService, VerificationService>();
builder.Services.AddScoped<IOTPService, OTPService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

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

app.UseCors(); // Applies CORS policies globally

app.UseAuthorization();

app.MapControllers();

app.Run();
