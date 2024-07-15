using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BringMeBackAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    CommunityRole = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CommunityAffiliation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Verification = table.Column<bool>(type: "bit", nullable: true),
                    DonationPreference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MessageOfSupport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentInformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelationToMissingPerson = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DetailsOfMissingPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadPhoto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    OrganizationName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    OrganizationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PositionOrAgency = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Authorization = table.Column<bool>(type: "bit", nullable: true),
                    AccessCredentials = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    VolunteerExperience = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Availability = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InterestArea = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Donation",
                columns: table => new
                {
                    DonationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationUserId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DonationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CommunityMemberId = table.Column<int>(type: "int", nullable: true),
                    DonorSupporterId = table.Column<int>(type: "int", nullable: true),
                    FamilyMemberId = table.Column<int>(type: "int", nullable: true),
                    PublicAuthorityId = table.Column<int>(type: "int", nullable: true),
                    VolunteerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donation", x => x.DonationId);
                    table.ForeignKey(
                        name: "FK_Donation_Users_CommunityMemberId",
                        column: x => x.CommunityMemberId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Donation_Users_DonorSupporterId",
                        column: x => x.DonorSupporterId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Donation_Users_FamilyMemberId",
                        column: x => x.FamilyMemberId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Donation_Users_OrganizationUserId",
                        column: x => x.OrganizationUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Donation_Users_PublicAuthorityId",
                        column: x => x.PublicAuthorityId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Donation_Users_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OTPs",
                columns: table => new
                {
                    OTPId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTPs", x => x.OTPId);
                    table.ForeignKey(
                        name: "FK_OTPs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ReportType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsResolved = table.Column<bool>(type: "bit", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    FoundItemReport_ItemName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FoundItemReport_ItemDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FoundItemReport_SerialNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FoundItemReport_UniqueIdentifiers = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FoundItemReport_FoundLocation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FoundItemReport_FoundDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConditionOfItemWhenFound = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReportingPersonName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ReportingPersonPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportingPersonEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FoundItemReport_PhotoOfItem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CircumstancesOfFinding = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FoundItemReport_VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FoundPersonReport_FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FoundPersonReport_Nickname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FoundPersonReport_Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    EstimatedAge = table.Column<int>(type: "int", nullable: true),
                    FoundPersonReport_Nationality = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FoundPersonReport_Height = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FoundPersonReport_Weight = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FoundPersonReport_EyeColor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FoundPersonReport_HairColor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FoundPersonReport_DistinguishingMarksOrFeatures = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FoundLocation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FoundDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClothingAtTimeOfFinding = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ConditionWhenFound = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ObservedMedicalConditions = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ObservedMedications = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ObservedMentalHealthStatus = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ItemName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ItemDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UniqueIdentifiers = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LastKnownLocation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ItemReport_LastSeenDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CircumstancesOfLoss = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    OwnerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OwnerPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoOfItem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstimatedValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RewardOffered = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ItemReport_VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Nickname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IDNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Height = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Weight = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EyeColor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HairColor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DistinguishingMarksOrFeatures = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LastSeenLocation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastSeenDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClothingLastSeenWearing = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PossibleDestinations = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MedicalConditions = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MedicationsRequired = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MentalHealthStatus = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PrimaryContactPerson = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SocialMediaAccounts = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecentPhotos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BriefDescriptionOfCircumstances = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_Reports_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Verifications",
                columns: table => new
                {
                    VerificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    VerificationCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verifications", x => x.VerificationId);
                    table.ForeignKey(
                        name: "FK_Verifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Associates",
                columns: table => new
                {
                    AssociateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Relationship = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Associates", x => x.AssociateId);
                    table.ForeignKey(
                        name: "FK_Associates_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "ReportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ParentCommentId = table.Column<int>(type: "int", nullable: true),
                    CommentId1 = table.Column<int>(type: "int", nullable: true),
                    ReportId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_CommentId1",
                        column: x => x.CommentId1,
                        principalTable: "Comments",
                        principalColumn: "CommentId");
                    table.ForeignKey(
                        name: "FK_Comments_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "ReportId");
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Associates_ReportId",
                table: "Associates",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentId1",
                table: "Comments",
                column: "CommentId1");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ReportId",
                table: "Comments",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Donation_CommunityMemberId",
                table: "Donation",
                column: "CommunityMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Donation_DonorSupporterId",
                table: "Donation",
                column: "DonorSupporterId");

            migrationBuilder.CreateIndex(
                name: "IX_Donation_FamilyMemberId",
                table: "Donation",
                column: "FamilyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Donation_OrganizationUserId",
                table: "Donation",
                column: "OrganizationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Donation_PublicAuthorityId",
                table: "Donation",
                column: "PublicAuthorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Donation_VolunteerId",
                table: "Donation",
                column: "VolunteerId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OTPs_UserId",
                table: "OTPs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_UserId",
                table: "Reports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Verifications_UserId",
                table: "Verifications",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Associates");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Donation");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "OTPs");

            migrationBuilder.DropTable(
                name: "Verifications");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
