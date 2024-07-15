using Microsoft.EntityFrameworkCore;
using BringMeBackAPI.Models.Users;
using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Models.Notifications;
using BringMeBackAPI.Models.Verification;
using BringMeBackAPI.Models.Associates;
using BringMeBackAPI.Models.Comments;

namespace BringMeBack.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {           
        }

        //Users
        public DbSet<User> Users { get; set; }
        public DbSet<OrganizationUser> OrganizationUsers { get; set; }
        public DbSet<CommunityMember> CommunityMembers { get; set; }
        public DbSet<FamilyMember> FamilyMembers { get; set; }
        public DbSet<PublicAuthority> PublicAuthorities { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<DonorSupporter> DonorSupporters { get; set; }

        //verification
        public DbSet<Verification> Verifications { get; set; }        
        public DbSet<OTP> OTPs { get; set; }

        //Notification
        public DbSet<Notification> Notifications { get; set; }

        //Reports
        public DbSet<Report> Reports { get; set; }
        public DbSet<PersonReport> PersonReports { get; set; }
        public DbSet<ItemReport> ItemReports { get; set; }
        public DbSet<FoundPersonReport> FoundPersonReports { get; set; }
        public DbSet<FoundItemReport> FoundItemReports { get; set; }

        //Associates
        public DbSet<Associate> Associates { get; set; }

        //comments
         public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Specify Database tables:
            // Users
            modelBuilder.Entity<User>()
                .ToTable("Users");

            modelBuilder.Entity<CommunityMember>()
                .ToTable("CommunityMembers");

            modelBuilder.Entity<DonorSupporter>()
                .ToTable("DonorSupporters");

            modelBuilder.Entity<FamilyMember>()
                .ToTable("FamilyMembers");

            modelBuilder.Entity<OrganizationUser>()
                .ToTable("OrganizationUsers");

            modelBuilder.Entity<PublicAuthority>()
              .ToTable("PublicAuthority");

            modelBuilder.Entity<Volunteer>()
                .ToTable("Volunteer");

            // Configure TPT inheritance strategy
            modelBuilder.Entity<Report>()
                .ToTable("Reports");

            modelBuilder.Entity<ItemReport>()
                .ToTable("ItemReports");

            modelBuilder.Entity<PersonReport>()
                .ToTable("PersonReports");

            modelBuilder.Entity<FoundPersonReport>()
                .ToTable("FoundPersonReports");

            modelBuilder.Entity<FoundItemReport>()
                .ToTable("FoundItemReport");

            //Seed Data For Users
            modelBuilder.Entity<CommunityMember>().HasData(
                new CommunityMember
                {
                    Id = 1,
                    Name = "John Doe",
                    Email = "john.doe@example.com",
                    Password = "password123",
                    PhoneNumber = "+27123456789",
                    Location = "Johannesburg",
                    Role = UserRole.CommunityMember,
                    CommunityRole = "Leader",
                    CommunityAffiliation = "Local Council",
                    Verification = true
                }
            );

            modelBuilder.Entity<DonorSupporter>().HasData(
                new DonorSupporter
                {
                    Id = 2,
                    Name = "Jane Smith",
                    Email = "jane.smith@example.com",
                    Password = "password123",
                    PhoneNumber = "+27876543210",
                    Location = "Cape Town",
                    Role = UserRole.DonorSupporter,
                    DonationPreference = "Monetary",
                    MessageOfSupport = "Keep up the great work!",
                    PaymentInformation = "PayPal"
                }
            );

            modelBuilder.Entity<FamilyMember>().HasData(
                new FamilyMember
                {
                    Id = 3,
                    Name = "Michael Brown",
                    Email = "michael.brown@example.com",
                    Password = "password123",
                    PhoneNumber = "+27109876543",
                    Location = "Durban",
                    Role = UserRole.FamilyMember,
                    RelationToMissingPerson = "Brother",
                    DetailsOfMissingPerson = "Missing since last month",
                    UploadPhoto = "https://example.com/photo.jpg"
                }
            );

            modelBuilder.Entity<OrganizationUser>().HasData(
                new OrganizationUser
                {
                    Id = 4,
                    Name = "Emily Johnson",
                    Email = "emily.johnson@example.com",
                    Password = "password123",
                    PhoneNumber = "+27654321098",
                    Location = "Pretoria",
                    Role = UserRole.Organization,
                    OrganizationName = "Help Foundation",
                    OrganizationType = "NGO",
                    RegistrationNumber = "1234567890",
                    Address = "123 Charity Lane",
                    ContactPerson = "Emily Johnson",
                    ContactPhoneNumber = "+27654321098",
                    ContactEmail = "contact@helpfoundation.org"
                }
            );

            modelBuilder.Entity<PublicAuthority>().HasData(
                new PublicAuthority
                {
                    Id = 5,
                    Name = "David Williams",
                    Email = "david.williams@example.com",
                    Password = "password123",
                    PhoneNumber = "+27456789012",
                    Location = "Port Elizabeth",
                    Role = UserRole.PublicAuthority,
                    PositionOrAgency = "Police Department",
                    Authorization = true,
                    AccessCredentials = "AuthToken12345"
                }
            );

            modelBuilder.Entity<Volunteer>().HasData(
                new Volunteer
                {
                    Id = 6,
                    Name = "Sarah Miller",
                    Email = "sarah.miller@example.com",
                    Password = "password123",
                    PhoneNumber = "+27345678901",
                    Location = "East London",
                    Role = UserRole.Volunteer,
                    VolunteerExperience = "3 years with Red Cross",
                    Availability = "Weekends",
                    InterestArea = "Child Welfare"
                }
            );

            //            ///////REPPORTS/////////
            //            modelBuilder.Entity<PersonReport>().HasData(
            //    new PersonReport
            //    {
            //        ReportId = 1,
            //        UserId = 1,
            //        ReportType = "MissingPerson",
            //        FullName = "Sarah Smith",
            //        Gender = "Female",
            //        DateOfBirth = new DateTime(1995, 5, 15),
            //        IDNumber = "9505151234567",
            //        Nationality = "South African",
            //        Height = "165 cm",
            //        Weight = "60 kg",
            //        EyeColor = "Brown",
            //        HairColor = "Black",
            //        DistinguishingMarksOrFeatures = "Scar on left cheek",
            //        LastSeenLocation = "Johannesburg CBD",
            //        LastSeenDateTime = new DateTime(2024, 6, 1, 15, 30, 0),
            //        ClothingLastSeenWearing = "Blue jeans, white t-shirt",
            //        PossibleDestinations = "Pretoria, Cape Town",
            //        ContactPhoneNumber = "+27123456789",
            //        ContactEmailAddress = "sarah.smith@example.com",
            //        SocialMediaAccounts = "@sarahsmith",
            //        RecentPhotos = new List<string> { "https://example.com/photo1.jpg", "https://example.com/photo2.jpg" },
            //        BriefDescriptionOfCircumstances = "Last seen near the bus stop on Oxford Street.",
            //        VideoUrl = "https://example.com/video"
            //    },
            //    new PersonReport
            //    {
            //        ReportId = 2,
            //        UserId = 2,
            //        ReportType = "FoundPerson",
            //        FullName = "David Johnson",
            //        Gender = "Male",
            //        DateOfBirth = new DateTime(1980, 8, 20),
            //        IDNumber = "8008201234567",
            //        Nationality = "South African",
            //        Height = "180 cm",
            //        Weight = "75 kg",
            //        EyeColor = "Blue",
            //        HairColor = "Brown",
            //        DistinguishingMarksOrFeatures = "Tattoo on right forearm",
            //        //FoundLocation = "Cape Town",
            //        //FoundDateTime = new DateTime(2024, 6, 10, 10, 0, 0),
            //        //ClothingAtTimeOfFinding = "Black jacket, grey pants",
            //        //ConditionWhenFound = "Appeared disoriented but unharmed.",
            //        //ObservedMedicalConditions = "None",
            //        //ObservedMedications = "None",
            //        //ObservedMentalHealthStatus = "Stressed but coherent."
            //    }
            //    // Add more PersonReports as needed
            //);

            //modelBuilder.Entity<ItemReport>().HasData(
            //    new ItemReport
            //    {
            //        ReportId = 3,
            //        UserId = 3,
            //        ReportType = "MissingItem",
            //        ItemName = "Laptop",
            //        ItemDescription = "Dell Inspiron 15",
            //        SerialNumber = "ABC123456789",
            //        UniqueIdentifiers = "Red laptop bag",
            //        LastKnownLocation = "Pretoria",
            //        LastSeenDateTime = new DateTime(2024, 7, 5, 12, 0, 0),
            //        CircumstancesOfLoss = "Stolen from office desk.",
            //        OwnerName = "Michael Brown",
            //        OwnerPhoneNumber = "+27765432109",
            //        OwnerEmailAddress = "michael.brown@example.com",
            //        PhotoOfItem = "https://example.com/laptop.jpg",
            //        EstimatedValue = 12000.00m,
            //        RewardOffered = 500.00m,
            //        VideoUrl = "https://example.com/video"
            //    },
            //    new ItemReport
            //    {
            //        ReportId = 4,
            //        UserId = 2,
            //        ReportType = "FoundItem",
            //        ItemName = "Mobile Phone",
            //        ItemDescription = "iPhone 12 Pro",
            //        SerialNumber = "XYZ987654321",
            //        UniqueIdentifiers = "Black phone case with blue stripes",
            //        //FoundLocation = "Durban",
            //        //FoundDateTime = new DateTime(2024, 7, 8, 9, 0, 0),
            //        //ConditionOfItemWhenFound = "Screen intact, no visible damage.",
            //        //ReportingPersonName = "Emily Johnson",
            //        //ReportingPersonPhoneNumber = "+27654321098",
            //        //ReportingPersonEmailAddress = "emily.johnson@example.com",
            //        PhotoOfItem = "https://example.com/phone.jpg",
            //        EstimatedValue = 15000.00m,
            //        RewardOffered = 300.00m,
            //        VideoUrl = "https://example.com/video"
            //    }
            //    // Add more ItemReports as needed
            //);

            //modelBuilder.Entity<FoundPersonReport>().HasData(
            //    new FoundPersonReport
            //    {
            //        ReportId = 5,
            //        UserId = 2,
            //        ReportType = "FoundPerson",
            //        FullName = "Sophie Taylor",
            //        Gender = "Female",
            //        EstimatedAge = 25,
            //        Nationality = "South African",
            //        Height = "170 cm",
            //        Weight = "65 kg",
            //        EyeColor = "Green",
            //        HairColor = "Blonde",
            //        DistinguishingMarksOrFeatures = "Birthmark on left forearm",
            //        FoundLocation = "Port Elizabeth",
            //        FoundDateTime = new DateTime(2024, 6, 20, 14, 0, 0),
            //        ClothingAtTimeOfFinding = "Red dress, white shoes",
            //        ConditionWhenFound = "Appeared confused but physically healthy.",
            //        ObservedMedicalConditions = "None",
            //        ObservedMedications = "None",
            //        ObservedMentalHealthStatus = "Agitated but responsive."
            //    },
            //    new FoundPersonReport
            //    {
            //        ReportId = 6,
            //        UserId = 6,
            //        ReportType = "FoundPerson",
            //        FullName = "Matthew Clark",
            //        Gender = "Male",
            //        EstimatedAge = 30,
            //        Nationality = "South African",
            //        Height = "185 cm",
            //        Weight = "80 kg",
            //        EyeColor = "Brown",
            //        HairColor = "Black",
            //        DistinguishingMarksOrFeatures = "Scar on forehead",
            //        FoundLocation = "Bloemfontein",
            //        FoundDateTime = new DateTime(2024, 7, 1, 11, 0, 0),
            //        ClothingAtTimeOfFinding = "White shirt, blue jeans",
            //        ConditionWhenFound = "Appeared disoriented but physically fit.",
            //        ObservedMedicalConditions = "None",
            //        ObservedMedications = "None",
            //        ObservedMentalHealthStatus = "Confused but cooperative."
            //    }
            //    // Add more FoundPersonReports as needed
            //);

            //modelBuilder.Entity<FoundItemReport>().HasData(
            //    new FoundItemReport
            //    {
            //        ReportId = 7,
            //        UserId = 3,
            //        ReportType = "FoundItem",
            //        ItemName = "Camera",
            //        ItemDescription = "Canon EOS 5D Mark IV",
            //        SerialNumber = "DEF456789012",
            //        UniqueIdentifiers = "Camera bag with 'Photography' tag",
            //        FoundLocation = "East London",
            //        FoundDateTime = new DateTime(2024, 7, 12, 16, 0, 0),
            //        ConditionOfItemWhenFound = "Lens intact, minor scratches on body.",
            //        ReportingPersonName = "James Anderson",
            //        ReportingPersonPhoneNumber = "+27456789012",
            //        ReportingPersonEmailAddress = "james.anderson@example.com",
            //        PhotoOfItem = "https://example.com/camera.jpg",
            //        CircumstancesOfFinding = "Found abandoned in park.",
            //        VideoUrl = "https://example.com/video"
            //    },
            //    new FoundItemReport
            //    {
            //        ReportId = 8,
            //        UserId = 4,
            //        ReportType = "FoundItem",
            //        ItemName = "Watch",
            //        ItemDescription = "Rolex Submariner",
            //        SerialNumber = "GHI789012345",
            //        UniqueIdentifiers = "Gold band with initials 'J.A.' engraved",
            //        FoundLocation = "Pretoria",
            //        FoundDateTime = new DateTime(2024, 7, 14, 10, 0, 0),
            //        ConditionOfItemWhenFound = "Good condition, slight wear on band.",
            //        ReportingPersonName = "Emma Thomas",
            //        ReportingPersonPhoneNumber = "+27345678901",
            //        ReportingPersonEmailAddress = "emma.thomas@example.com",
            //        PhotoOfItem = "https://example.com/watch.jpg",
            //        CircumstancesOfFinding = "Found near bus station.",
            //        VideoUrl = "https://example.com/video"
            //    }
            //    // Add more FoundItemReports as needed
            //);
        }
    }
}
