using Microsoft.EntityFrameworkCore;
using BringMeBackAPI.Models.Users;
using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Models.Notifications;
using BringMeBackAPI.Models.Verification;
using BringMeBackAPI.Models.Associates;
using BringMeBackAPI.Models.Comments;
using BringMeBackAPI.Models.Reports.Dashboard;

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
        public DbSet<MissingPersonReport> MissingPersonReports { get; set; }
        public DbSet<MissingItemReport> MissingItemReports { get; set; }
        public DbSet<FoundPersonReport> FoundPersonReports { get; set; }
        public DbSet<FoundItemReport> FoundItemReports { get; set; }

        //Dashboard
        public DbSet<MissingPersonReportFilterParams> MissingPersonReportFilterParamss { get; set; }

        //Associates
        public DbSet<Associate> Associates { get; set; }

        //comments
         public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure keyless entity types
            modelBuilder.Entity<MissingPersonReportFilterParams>().HasNoKey();

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

            modelBuilder.Entity<MissingItemReport>()
                .ToTable("MissingItemReports");

            modelBuilder.Entity<MissingPersonReport>()
                .ToTable("MissingPersonReports");

            modelBuilder.Entity<FoundPersonReport>()
                .ToTable("FoundPersonReports");

            modelBuilder.Entity<FoundItemReport>()
                .ToTable("FoundItemReport");

            // Configure the relationships
            modelBuilder.Entity<Associate>()
                .HasOne(a => a.Report)
                .WithMany(r => r.Associates)
                .HasForeignKey(a => a.ReportId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Associate>()
                .HasOne(a => a.Report)
                .WithMany(pr => pr.Associates)
                .HasForeignKey(a => a.ReportId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ParentComment)
                .WithMany(pc => pc.Replies)
                .HasForeignKey(c => c.ParentCommentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Report)
                .WithMany(r => r.Comments)
                .HasForeignKey(c => c.ReportId)
                .OnDelete(DeleteBehavior.Restrict);

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

    ////////MISSING PERSON SEEDING//////////
            modelBuilder.Entity<MissingPersonReport>().HasData(
        new MissingPersonReport
        {
            ReportId = 1,
            UserId = 1, // Assign this to an existing user Id
            ReportType = "MissingPerson",
            Description = "John Doe, missing since last week.",
            CreatedAt = DateTime.UtcNow,
            IsResolved = false,
            FullName = "John Doe",
            Nickname = "Johnny",
            Gender = "Male",
            DateOfBirth = new DateTime(1990, 1, 1),
            IDNumber = "9001011234567",
            Nationality = "South African",
            Height = "6'0\"",
            Weight = "180 lbs",
            EyeColor = "Brown",
            HairColor = "Black",
            DistinguishingMarksOrFeatures = "Tattoo on left arm",
            LastSeenLocation = "Johannesburg CBD",
            LastSeenDateTime = DateTime.UtcNow.AddDays(-7),
            ClothingLastSeenWearing = "Blue jeans, white t-shirt",
            PossibleDestinations = "Home, friend's place",
            MedicalConditions = "None",
            MedicationsRequired = "None",
            MentalHealthStatus = "Stable",
            PrimaryContactPerson = "Jane Doe",
            ContactPhoneNumber = "+27123456789",
            ContactEmailAddress = "jane.doe@example.com",
            SocialMediaAccounts = "Facebook: fb.com/johndoe",
            RecentPhotos = new List<string> { "https://example.com/photo1.jpg", "https://example.com/photo2.jpg" },
            BriefDescriptionOfCircumstances = "Last seen near a local park.",
            VideoUrl = "https://example.com/video.mp4"
        },
        new MissingPersonReport
        {
            ReportId = 2,
            UserId = 2, // Assign this to another existing user Id
            ReportType = "MissingPerson",
            Description = "Jane Smith, missing since yesterday.",
            CreatedAt = DateTime.UtcNow,
            IsResolved = false,
            FullName = "Jane Smith",
            Nickname = "Jane",
            Gender = "Female",
            DateOfBirth = new DateTime(1985, 5, 10),
            IDNumber = "8505100123456",
            Nationality = "British",
            Height = "5'6\"",
            Weight = "140 lbs",
            EyeColor = "Blue",
            HairColor = "Blonde",
            DistinguishingMarksOrFeatures = "Scar on right cheek",
            LastSeenLocation = "Cape Town Waterfront",
            LastSeenDateTime = DateTime.UtcNow.AddDays(-1),
            ClothingLastSeenWearing = "Red dress, black heels",
            PossibleDestinations = "Workplace, home",
            MedicalConditions = "Allergies to nuts",
            MedicationsRequired = "EpiPen",
            MentalHealthStatus = "Anxiety disorder",
            PrimaryContactPerson = "John Smith",
            ContactPhoneNumber = "+27876543210",
            ContactEmailAddress = "john.smith@example.com",
            SocialMediaAccounts = "Twitter: @janesmith",
            RecentPhotos = new List<string> { "https://example.com/janesphoto1.jpg", "https://example.com/janesphoto2.jpg" },
            BriefDescriptionOfCircumstances = "Last seen near the waterfront area.",
            VideoUrl = "https://example.com/missingjane.mp4"
        },
        new MissingPersonReport
        {
            ReportId = 3,
            UserId = 3, // Assign this to another existing user Id
            ReportType = "FoundPerson",
            Description = "Mary Johnson, found near Durban.",
            CreatedAt = DateTime.UtcNow,
            IsResolved = true,
            FullName = "Mary Johnson",
            Nickname = "MJ",
            Gender = "Female",
            DateOfBirth = new DateTime(1998, 8, 15),
            IDNumber = "9808155432109",
            Nationality = "South African",
            Height = "5'8\"",
            Weight = "160 lbs",
            EyeColor = "Green",
            HairColor = "Brown",
            DistinguishingMarksOrFeatures = "None",
            LastSeenLocation = "Durban",
            LastSeenDateTime = DateTime.UtcNow.AddDays(-2),
            ClothingLastSeenWearing = "White shirt, blue jeans",
            PossibleDestinations = "Home, friend's place",
            MedicalConditions = "None",
            MedicationsRequired = "None",
            MentalHealthStatus = "Healthy",
            PrimaryContactPerson = "Tom Johnson",
            ContactPhoneNumber = "+27765432109",
            ContactEmailAddress = "tom.johnson@example.com",
            SocialMediaAccounts = "LinkedIn: linkedin.com/maryjohnson",
            RecentPhotos = new List<string> { "https://example.com/marysphoto1.jpg", "https://example.com/marysphoto2.jpg" },
            BriefDescriptionOfCircumstances = "Found near the local mall.",
            VideoUrl = "https://example.com/foundmary.mp4"
        },
        new MissingPersonReport
        {
            ReportId = 4,
            UserId = 4, // Assign this to another existing user Id
            ReportType = "MissingPerson",
            Description = "David Brown, missing since last month.",
            CreatedAt = DateTime.UtcNow,
            IsResolved = false,
            FullName = "David Brown",
            Nickname = "DB",
            Gender = "Male",
            DateOfBirth = new DateTime(1980, 4, 20),
            IDNumber = "8004201234567",
            Nationality = "South African",
            Height = "5'10\"",
            Weight = "170 lbs",
            EyeColor = "Brown",
            HairColor = "Black",
            DistinguishingMarksOrFeatures = "Scar on left forearm",
            LastSeenLocation = "Johannesburg",
            LastSeenDateTime = DateTime.UtcNow.AddDays(-30),
            ClothingLastSeenWearing = "Grey jacket, blue jeans",
            PossibleDestinations = "Home, workplace",
            MedicalConditions = "Diabetes",
            MedicationsRequired = "Insulin",
            MentalHealthStatus = "Stable",
            PrimaryContactPerson = "Emily Brown",
            ContactPhoneNumber = "+27654321098",
            ContactEmailAddress = "emily.brown@example.com",
            SocialMediaAccounts = "Instagram: @davidbrown",
            RecentPhotos = new List<string> { "https://example.com/davidsphoto1.jpg", "https://example.com/davidsphoto2.jpg" },
            BriefDescriptionOfCircumstances = "Last seen in the downtown area.",
            VideoUrl = "https://example.com/missingdavid.mp4"
        },
        new MissingPersonReport
        {
            ReportId = 5,
            UserId = 5, // Assign this to another existing user Id
            ReportType = "MissingPerson",
            Description = "Alice White, missing since last week.",
            CreatedAt = DateTime.UtcNow,
            IsResolved = false,
            FullName = "Alice White",
            Nickname = "Al",
            Gender = "Female",
            DateOfBirth = new DateTime(1995, 3, 25),
            IDNumber = "9503259876543",
            Nationality = "South African",
            Height = "5'4\"",
            Weight = "130 lbs",
            EyeColor = "Brown",
            HairColor = "Brown",
            DistinguishingMarksOrFeatures = "Birthmark on right cheek",
            LastSeenLocation = "Pretoria",
            LastSeenDateTime = DateTime.UtcNow.AddDays(-7),
            ClothingLastSeenWearing = "Black dress, sandals",
            PossibleDestinations = "Home, friend's house",
            MedicalConditions = "None",
            MedicationsRequired = "None",
            MentalHealthStatus = "Stable",
            PrimaryContactPerson = "Robert White",
            ContactPhoneNumber = "+27780123456",
            ContactEmailAddress = "robert.white@example.com",
            SocialMediaAccounts = "Facebook: fb.com/alicewhite",
            RecentPhotos = new List<string> { "https://example.com/alicesphoto1.jpg", "https://example.com/alicesphoto2.jpg" },
            BriefDescriptionOfCircumstances = "Last seen near the park area.",
            VideoUrl = "https://example.com/missingalice.mp4"
        },
        new MissingPersonReport
        {
            ReportId = 6,
            UserId = 6, // Assign this to another existing user Id
            ReportType = "MissingPerson",
            Description = "Peter Green, missing since yesterday.",
            CreatedAt = DateTime.UtcNow,
            IsResolved = false,
            FullName = "Peter Green",
            Nickname = "Pete",
            Gender = "Male",
            DateOfBirth = new DateTime(1987, 7, 12),
            IDNumber = "8707128765432",
            Nationality = "South African",
            Height = "5'9\"",
            Weight = "160 lbs",
            EyeColor = "Green",
            HairColor = "Brown",
            DistinguishingMarksOrFeatures = "None",
            LastSeenLocation = "Cape Town CBD",
            LastSeenDateTime = DateTime.UtcNow.AddDays(-1),
            ClothingLastSeenWearing = "Grey hoodie, jeans",
            PossibleDestinations = "Home, friend's place",
            MedicalConditions = "None",
            MedicationsRequired = "None",
            MentalHealthStatus = "Stable",
            PrimaryContactPerson = "Sarah Green",
            ContactPhoneNumber = "+27817654321",
            ContactEmailAddress = "sarah.green@example.com",
            SocialMediaAccounts = "Twitter: @petergreen",
            RecentPhotos = new List<string> { "https://example.com/petersphoto1.jpg", "https://example.com/petersphoto2.jpg" },
            BriefDescriptionOfCircumstances = "Last seen near the shopping center.",
            VideoUrl = "https://example.com/missingpeter.mp4"
        },
        new MissingPersonReport
        {
            ReportId = 7,
            UserId = 1, // Assign this to another existing user Id
            ReportType = "FoundPerson",
            Description = "Emma Lee, found near Johannesburg.",
            CreatedAt = DateTime.UtcNow,
            IsResolved = true,
            FullName = "Emma Lee",
            Nickname = "Lee",
            Gender = "Female",
            DateOfBirth = new DateTime(1992, 11, 5),
            IDNumber = "9211056543210",
            Nationality = "South African",
            Height = "5'7\"",
            Weight = "150 lbs",
            EyeColor = "Brown",
            HairColor = "Blonde",
            DistinguishingMarksOrFeatures = "None",
            LastSeenLocation = "Johannesburg",
            LastSeenDateTime = DateTime.UtcNow.AddDays(-3),
            ClothingLastSeenWearing = "Black jacket, blue jeans",
            PossibleDestinations = "Home, friend's house",
            MedicalConditions = "None",
            MedicationsRequired = "None",
            MentalHealthStatus = "Healthy",
            PrimaryContactPerson = "Mark Lee",
            ContactPhoneNumber = "+27654321876",
            ContactEmailAddress = "mark.lee@example.com",
            SocialMediaAccounts = "Instagram: @emmalee",
            RecentPhotos = new List<string> { "https://example.com/emmasphoto1.jpg", "https://example.com/emmasphoto2.jpg" },
            BriefDescriptionOfCircumstances = "Found near the city center.",
            VideoUrl = "https://example.com/foundemma.mp4"
        },
        new MissingPersonReport
        {
            ReportId = 8,
            UserId = 2, // Assign this to another existing user Id
            ReportType = "MissingPerson",
            Description = "Michael Grey, missing since yesterday.",
            CreatedAt = DateTime.UtcNow,
            IsResolved = false,
            FullName = "Michael Grey",
            Nickname = "Mic",
            Gender = "Male",
            DateOfBirth = new DateTime(1983, 9, 30),
            IDNumber = "8309307654321",
            Nationality = "South African",
            Height = "5'11\"",
            Weight = "175 lbs",
            EyeColor = "Blue",
            HairColor = "Brown",
            DistinguishingMarksOrFeatures = "Tattoo on right arm",
            LastSeenLocation = "Durban",
            LastSeenDateTime = DateTime.UtcNow.AddDays(-1),
            ClothingLastSeenWearing = "White shirt, black trousers",
            PossibleDestinations = "Home, workplace",
            MedicalConditions = "None",
            MedicationsRequired = "None",
            MentalHealthStatus = "Stable",
            PrimaryContactPerson = "Michelle Grey",
            ContactPhoneNumber = "+27765432189",
            ContactEmailAddress = "michelle.grey@example.com",
            SocialMediaAccounts = "LinkedIn: linkedin.com/michaelgrey",
            RecentPhotos = new List<string> { "https://example.com/michaelsphoto1.jpg", "https://example.com/michaelsphoto2.jpg" },
            BriefDescriptionOfCircumstances = "Last seen near the beach area.",
            VideoUrl = "https://example.com/missingmichael.mp4"
        },
        new MissingPersonReport
        {
            ReportId = 9,
            UserId = 3, // Assign this to another existing user Id
            ReportType = "FoundPerson",
            Description = "Sophia Clark, found near Port Elizabeth.",
            CreatedAt = DateTime.UtcNow,
            IsResolved = true,
            FullName = "Sophia Clark",
            Nickname = "Sophy",
            Gender = "Female",
            DateOfBirth = new DateTime(1996, 6, 18),
            IDNumber = "9606189876543",
            Nationality = "South African",
            Height = "5'6\"",
            Weight = "145 lbs",
            EyeColor = "Brown",
            HairColor = "Brown",
            DistinguishingMarksOrFeatures = "None",
            LastSeenLocation = "Port Elizabeth",
            LastSeenDateTime = DateTime.UtcNow.AddDays(-2),
            ClothingLastSeenWearing = "Green blouse, black skirt",
            PossibleDestinations = "Home, friend's house",
            MedicalConditions = "None",
            MedicationsRequired = "None",
            MentalHealthStatus = "Healthy",
            PrimaryContactPerson = "Daniel Clark",
            ContactPhoneNumber = "+27891234567",
            ContactEmailAddress = "daniel.clark@example.com",
            SocialMediaAccounts = "Facebook: fb.com/sophiaclark",
            RecentPhotos = new List<string> { "https://example.com/sophiasphoto1.jpg", "https://example.com/sophiasphoto2.jpg" },
            BriefDescriptionOfCircumstances = "Found near the shopping mall.",
            VideoUrl = "https://example.com/foundsophia.mp4"
        },
        new MissingPersonReport
        {
            ReportId = 10,
            UserId = 4, // Assign this to another existing user Id
            ReportType = "MissingPerson",
            Description = "Robert Taylor, missing since last month.",
            CreatedAt = DateTime.UtcNow,
            IsResolved = false,
            FullName = "Robert Taylor",
            Nickname = "Rob",
            Gender = "Male",
            DateOfBirth = new DateTime(1975, 12, 8),
            IDNumber = "7512085432198",
            Nationality = "South African",
            Height = "6'2\"",
            Weight = "190 lbs",
            EyeColor = "Green",
            HairColor = "Brown",
            DistinguishingMarksOrFeatures = "None",
            LastSeenLocation = "Pretoria",
            LastSeenDateTime = DateTime.UtcNow.AddDays(-30),
            ClothingLastSeenWearing = "Black suit, white shirt",
            PossibleDestinations = "Home, workplace",
            MedicalConditions = "None",
            MedicationsRequired = "None",
            MentalHealthStatus = "Stable",
            PrimaryContactPerson = "Sarah Taylor",
            ContactPhoneNumber = "+27780123456",
            ContactEmailAddress = "sarah.taylor@example.com",
            SocialMediaAccounts = "Twitter: @roberttaylor",
            RecentPhotos = new List<string> { "https://example.com/robertsphoto1.jpg", "https://example.com/robertsphoto2.jpg" },
            BriefDescriptionOfCircumstances = "Last seen near the stadium area.",
            VideoUrl = "https://example.com/missingrobert.mp4"
        }
        );

            ////////MISSING ITEM SEEDING//////////
            modelBuilder.Entity<MissingItemReport>().HasData(
        new MissingItemReport
        {
            ReportId = 18,
            UserId = 1, // Assign this to an existing user Id
            ReportType = "LostItem",
            Description = "Missing laptop bag with important documents.",
            CreatedAt = DateTime.UtcNow,
            IsResolved = false,
            ItemName = "Laptop Bag",
            ItemDescription = "Black laptop bag with company logo.",
            SerialNumber = "ABC123XYZ",
            UniqueIdentifiers = "Company logo on front pocket.",
            LastKnownLocation = "Cape Town Airport",
            LastSeenDateTime = DateTime.UtcNow.AddDays(-1),
            CircumstancesOfLoss = "Left on the seat in departure lounge.",
            OwnerName = "John Doe",
            OwnerPhoneNumber = "+27817654321",
            OwnerEmailAddress = "john.doe@example.com",
            
            EstimatedValue = 500.00m,
            RewardOffered = 100.00m,
            VideoUrl = "https://example.com/lostlaptop.mp4"
        },
        new MissingItemReport
        {
            ReportId =19,
            UserId = 2, // Assign this to another existing user Id
            ReportType = "LostItem",
            Description = "Missing smartphone.",
            CreatedAt = DateTime.UtcNow,
            IsResolved = false,
            ItemName = "Smartphone",
            ItemDescription = "Black smartphone with cracked screen.",
            SerialNumber = "XYZ456ABC",
            UniqueIdentifiers = "Cracked screen, silver case.",
            LastKnownLocation = "Johannesburg CBD",
            LastSeenDateTime = DateTime.UtcNow.AddDays(-3),
            CircumstancesOfLoss = "Dropped on the street during rush hour.",
            OwnerName = "Jane Smith",
            OwnerPhoneNumber = "+27780123456",
            OwnerEmailAddress = "jane.smith@example.com",
            
            EstimatedValue = 300.00m,
            RewardOffered = 50.00m,
            VideoUrl = "https://example.com/lostphone.mp4"
        },
        new MissingItemReport
        {
            ReportId = 20,
            UserId = 3, // Assign this to another existing user Id
            ReportType = "FoundItem",
            Description = "Found camera near Durban.",
            CreatedAt = DateTime.UtcNow,
            IsResolved = true,
            ItemName = "Camera",
            ItemDescription = "Canon DSLR camera with zoom lens.",
            SerialNumber = "DEF789GHI",
            UniqueIdentifiers = "Zoom lens attached, initials engraved.",
            LastKnownLocation = "Durban Beachfront",
            LastSeenDateTime = DateTime.UtcNow.AddDays(-2),
            CircumstancesOfLoss = "Left on a bench near the pier.",
            OwnerName = "Tom Johnson",
            OwnerPhoneNumber = "+27654321098",
            OwnerEmailAddress = "tom.johnson@example.com",
            
            EstimatedValue = 800.00m,
            RewardOffered = null, // No reward offered for found item
            VideoUrl = "https://example.com/foundcamera.mp4"
        },
        new MissingItemReport
        {
            ReportId = 21,
            UserId = 4, // Assign this to another existing user Id
            ReportType = "LostItem",
            Description = "Missing wristwatch.",
            CreatedAt = DateTime.UtcNow,
            IsResolved = false,
            ItemName = "Wristwatch",
            ItemDescription = "Gold wristwatch with leather strap.",
            SerialNumber = "1234567890",
            UniqueIdentifiers = "Engraved initials on the back.",
            LastKnownLocation = "Pretoria",
            LastSeenDateTime = DateTime.UtcNow.AddDays(-5),
            CircumstancesOfLoss = "Left on the bathroom counter at a restaurant.",
            OwnerName = "Emily Brown",
            OwnerPhoneNumber = "+27123456789",
            OwnerEmailAddress = "emily.brown@example.com",
            
            EstimatedValue = 400.00m,
            RewardOffered = 50.00m,
            VideoUrl = "https://example.com/lostwatch.mp4"
        },
        new MissingItemReport
        {
            ReportId = 22,
            UserId = 5, // Assign this to another existing user Id
            ReportType = "LostItem",
            Description = "Missing bicycle.",
            CreatedAt = DateTime.UtcNow,
            IsResolved = false,
            ItemName = "Bicycle",
            ItemDescription = "Red mountain bike with black seat.",
            SerialNumber = "7890123456",
            UniqueIdentifiers = "Black seat cover, red bell.",
            LastKnownLocation = "Cape Town",
            LastSeenDateTime = DateTime.UtcNow.AddDays(-7),
            CircumstancesOfLoss = "Stolen from outside office building.",
            OwnerName = "Robert White",
            OwnerPhoneNumber = "+27876543210",
            OwnerEmailAddress = "robert.white@example.com",
            
            EstimatedValue = 600.00m,
            RewardOffered = 100.00m,
            VideoUrl = "https://example.com/lostbike.mp4"
        }
    );
            ////////FOUND PERSON SEEDING//////////
            modelBuilder.Entity<FoundPersonReport>().HasData(
      new FoundPersonReport
      {
          ReportId = 11,
          UserId = 6, // Assign this to an existing user Id
          ReportType = "FoundPerson",
          Description = "Found a lost child near the park.",
          CreatedAt = DateTime.UtcNow,
          IsResolved = true,
          FullName = "Sophia Brown",
          Nickname = "Johnny",
          Gender = "Female",
          EstimatedAge = 8,
          Nationality = "South African",
          Height = "4'0\"",
          Weight = "60 lbs",
          EyeColor = "Brown",
          HairColor = "Black",
          DistinguishingMarksOrFeatures = "None",
          FoundLocation = "Johannesburg Park",
          FoundDateTime = DateTime.UtcNow.AddDays(-1),
          ClothingAtTimeOfFinding = "Pink dress, white shoes",
          ConditionWhenFound = "Healthy and calm",
          ObservedMedicalConditions = "None",
          ObservedMedications = "None",
          ObservedMentalHealthStatus = "Normal"
      },
      new FoundPersonReport
      {
          ReportId = 12,
          UserId = 5, // Assign this to another existing user Id
          ReportType = "FoundPerson",
          Description = "Found an elderly person wandering near the hospital.",
          CreatedAt = DateTime.UtcNow,
          IsResolved = true,
          FullName = "John Smith",
          Nickname = "Johnny",
          Gender = "Male",
          EstimatedAge = 70,
          Nationality = "South African",
          Height = "5'8\"",
          Weight = "150 lbs",
          EyeColor = "Blue",
          HairColor = "Grey",
          DistinguishingMarksOrFeatures = "Wearing glasses",
          FoundLocation = "Durban Hospital",
          FoundDateTime = DateTime.UtcNow.AddDays(-2),
          ClothingAtTimeOfFinding = "Blue trousers, white shirt",
          ConditionWhenFound = "Seems confused but physically well",
          ObservedMedicalConditions = "High blood pressure",
          ObservedMedications = "Blood pressure medication",
          ObservedMentalHealthStatus = "Confused"
      },
      new FoundPersonReport
      {
          ReportId = 13,
          UserId = 4, // Assign this to another existing user Id
          ReportType = "FoundPerson",
          Description = "Found a lost teenager near the mall.",
          CreatedAt = DateTime.UtcNow,
          IsResolved = true,
          FullName = "Emily Johnson",
          Nickname = "Johnny",
          Gender = "Female",
          EstimatedAge = 16,
          Nationality = "South African",
          Height = "5'5\"",
          Weight = "120 lbs",
          EyeColor = "Green",
          HairColor = "Brown",
          DistinguishingMarksOrFeatures = "None",
          FoundLocation = "Cape Town Mall",
          FoundDateTime = DateTime.UtcNow.AddDays(-3),
          ClothingAtTimeOfFinding = "Black jeans, red jacket",
          ConditionWhenFound = "Nervous but physically well",
          ObservedMedicalConditions = "None",
          ObservedMedications = "None",
          ObservedMentalHealthStatus = "Anxious"
      },
      new FoundPersonReport
      {
          ReportId = 14,
          UserId = 2, // Assign this to another existing user Id
          ReportType = "FoundPerson",
          Description = "Found a missing elderly woman near her home.",
          CreatedAt = DateTime.UtcNow,
          IsResolved = true,
          FullName = "Margaret White",
          Nickname = "Johnny",
          Gender = "Female",
          EstimatedAge = 75,
          Nationality = "South African",
          Height = "5'2\"",
          Weight = "130 lbs",
          EyeColor = "Brown",
          HairColor = "White",
          DistinguishingMarksOrFeatures = "Carrying a walking stick",
          FoundLocation = "Pretoria Suburbs",
          FoundDateTime = DateTime.UtcNow.AddDays(-4),
          ClothingAtTimeOfFinding = "Flowery dress, sandals",
          ConditionWhenFound = "Tired but in good spirits",
          ObservedMedicalConditions = "Arthritis",
          ObservedMedications = "Pain relievers",
          ObservedMentalHealthStatus = "Content"
      },
      new FoundPersonReport
      {
          ReportId = 15,
          UserId = 1, // Assign this to another existing user Id
          ReportType = "FoundPerson",
          Description = "Found a missing young adult near the university.",
          CreatedAt = DateTime.UtcNow,
          IsResolved = true,
          FullName = "David Lee",
          Nickname = "Johnny",
          Gender = "Male",
          EstimatedAge = 20,
          Nationality = "South African",
          Height = "6'0\"",
          Weight = "160 lbs",
          EyeColor = "Brown",
          HairColor = "Black",
          DistinguishingMarksOrFeatures = "Tattoo on left arm",
          FoundLocation = "Johannesburg University",
          FoundDateTime = DateTime.UtcNow.AddDays(-5),
          ClothingAtTimeOfFinding = "Blue hoodie, jeans",
          ConditionWhenFound = "Tired but physically well",
          ObservedMedicalConditions = "None",
          ObservedMedications = "None",
          ObservedMentalHealthStatus = "Exhausted"
      },
      new FoundPersonReport
      {
          ReportId = 16,
          UserId = 2, // Assign this to another existing user Id
          ReportType = "FoundPerson",
          Description = "Found a missing child near the sports stadium.",
          CreatedAt = DateTime.UtcNow,
          IsResolved = true,
          FullName = "Sophie Taylor",
          Nickname = "Johnny",
          Gender = "Female",
          EstimatedAge = 5,
          Nationality = "South African",
          Height = "3'5\"",
          Weight = "40 lbs",
          EyeColor = "Blue",
          HairColor = "Blonde",
          DistinguishingMarksOrFeatures = "Wearing a pink hat",
          FoundLocation = "Cape Town Stadium",
          FoundDateTime = DateTime.UtcNow.AddDays(-6),
          ClothingAtTimeOfFinding = "Pink dress, white shoes",
          ConditionWhenFound = "Crying but physically well",
          ObservedMedicalConditions = "None",
          ObservedMedications = "None",
          ObservedMentalHealthStatus = "Distressed"
      },
      new FoundPersonReport
      {
          ReportId = 17,
          UserId = 3, // Assign this to another existing user Id
          ReportType = "FoundPerson",
          Description = "Found a missing elderly man near the retirement village.",
          CreatedAt = DateTime.UtcNow,
          IsResolved = true,
          FullName = "George Brown",
          Nickname = "Johnny",
          Gender = "Male",
          EstimatedAge = 80,
          Nationality = "South African",
          Height = "5'6\"",
          Weight = "140 lbs",
          EyeColor = "Brown",
          HairColor = "Grey",
          DistinguishingMarksOrFeatures = "Wearing a hat",
          FoundLocation = "Durban Retirement Village",
          FoundDateTime = DateTime.UtcNow.AddDays(-7),
          ClothingAtTimeOfFinding = "Brown trousers, white shirt",
          ConditionWhenFound = "Confused but physically well",
          ObservedMedicalConditions = "Alzheimer's",
          ObservedMedications = "Memory enhancers",
          ObservedMentalHealthStatus = "Confused"
      }
  );
            ////////FOUND ITEM SEEDING//////////
            modelBuilder.Entity<FoundItemReport>().HasData(
       new FoundItemReport
       {
           ReportId = 23,
           UserId = 1, // Assign this to an existing user Id
           ReportType = "FoundItem",
           Description = "Found laptop bag with company logo.",
           CreatedAt = DateTime.UtcNow,
           IsResolved = false,
           ItemName = "Laptop Bag",
           ItemDescription = "Black laptop bag with company logo.",
           SerialNumber = "ABC123XYZ",
           UniqueIdentifiers = "Company logo on front pocket.",
           FoundLocation = "Cape Town Airport",
           FoundDateTime = DateTime.UtcNow.AddDays(-1),
           ConditionOfItemWhenFound = "Slightly scuffed on corners.",
           ReportingPersonName = "Mark Johnson",
           ReportingPersonPhoneNumber = "+27817654321",
           ReportingPersonEmailAddress = "mark.johnson@example.com",
           
           CircumstancesOfFinding = "Found on a seat in the departure lounge.",
           VideoUrl = "https://example.com/foundlaptopbag.mp4"
       },
       new FoundItemReport
       {
           ReportId = 24,
           UserId = 2, // Assign this to another existing user Id
           ReportType = "FoundItem",
           Description = "Found smartphone on the street.",
           CreatedAt = DateTime.UtcNow,
           IsResolved = false,
           ItemName = "Smartphone",
           ItemDescription = "Black smartphone with cracked screen.",
           SerialNumber = "XYZ456ABC",
           UniqueIdentifiers = "Cracked screen, silver case.",
           FoundLocation = "Johannesburg CBD",
           FoundDateTime = DateTime.UtcNow.AddDays(-3),
           ConditionOfItemWhenFound = "Screen cracked but functional.",
           ReportingPersonName = "Anna Smith",
           ReportingPersonPhoneNumber = "+27780123456",
           ReportingPersonEmailAddress = "anna.smith@example.com",
           
           CircumstancesOfFinding = "Found lying on the sidewalk.",
           VideoUrl = "https://example.com/foundsmartphone.mp4"
       },
       new FoundItemReport
       {
           ReportId = 25,
           UserId = 3, // Assign this to another existing user Id
           ReportType = "FoundItem",
           Description = "Found wallet in the park.",
           CreatedAt = DateTime.UtcNow,
           IsResolved = false,
           ItemName = "Wallet",
           ItemDescription = "Brown leather wallet with initials.",
           SerialNumber = "DEF789GHI",
           UniqueIdentifiers = "Initials engraved inside.",
           FoundLocation = "Central Park",
           FoundDateTime = DateTime.UtcNow.AddDays(-2),
           ConditionOfItemWhenFound = "Contents intact, no cash inside.",
           ReportingPersonName = "Sophie Brown",
           ReportingPersonPhoneNumber = "+27654321098",
           ReportingPersonEmailAddress = "sophie.brown@example.com",
           
           CircumstancesOfFinding = "Found near the bench by the lake.",
           VideoUrl = "https://example.com/foundwallet.mp4"
       }
   );

            ///////////ASSOCIATES///////
            ///
            modelBuilder.Entity<Associate>().HasData(
        // Link Associate to an existing MissingPersonReport (ReportId = 1)
        new Associate
        {
            AssociateId = 1,
            ReportId = 1, // Linking to John Doe's report
            OtherReportId = 2, // Linking to an existing MissingItemReport, for example
            Description = "Jane Doe",
            Relationship = "Spouse"
        },
        new Associate
        {
            AssociateId = 2,
            ReportId = 1, // Linking to John Doe's report
            OtherReportId = 3, // Linking to another existing report
            Description = "Tom Smith",
            Relationship = "Friend"
        },
        new Associate
        {
            AssociateId = 3,
            ReportId = 1, // Linking to John Doe's report
            OtherReportId = 4, // No link to other report
            Description = "Mary Johnson",
            Relationship = "Colleague"
        },
        // You can create more associates as needed, linking them to existing reports
        new Associate
        {
            AssociateId = 4,
            ReportId = 3, // New report will be created later
            OtherReportId = 1, // Linking to an existing MissingPersonReport
            Description = "Alex Brown",
            Relationship = "Cousin"
        },
        new Associate
        {
            AssociateId = 5,
            ReportId = 2, // New report will be created later
            OtherReportId = 2, // Linking to an existing MissingItemReport
            Description = "Laura Green",
            Relationship = "Sister"
        }
    );

            /////////COMMENTS////////
            modelBuilder.Entity<Comment>().HasData(
      new Comment
      {
          CommentId = 1,
          UserId = 2, // Assign this to an existing user Id
          Content = "Hope John is found soon.",
          CreatedAt = DateTime.UtcNow,
          ReportId = 1
      },
      new Comment
      {
          CommentId = 2,
          UserId = 3, // Assign this to another existing user Id
          Content = "Please contact me if there's any update.",
          CreatedAt = DateTime.UtcNow.AddDays(-1),
          ReportId = 1,
          ParentCommentId = 1 // Reply to CommentId = 1
      },
      new Comment
      {
          CommentId = 3,
          UserId = 4, // Assign this to another existing user Id
          Content = "Praying for his safe return.",
          CreatedAt = DateTime.UtcNow.AddDays(-2),
          ReportId = 1
      },
      // Replies
      new Comment
      {
          CommentId = 4,
          UserId = 1, // Assign this to another existing user Id
          Content = "Thank you!",
          CreatedAt = DateTime.UtcNow,
          ReportId = 1,
          ParentCommentId = 3 // Reply to CommentId = 3
      },
      new Comment
      {
          CommentId = 5,
          UserId = 2, // Assign this to another existing user Id
          Content = "Keeping an eye out in the area.",
          CreatedAt = DateTime.UtcNow.AddDays(-1),
          ReportId = 1,
          ParentCommentId = 3 // Reply to CommentId = 3
      },
      new Comment
      {
          CommentId = 6,
          UserId = 3, // Assign this to another existing user Id
          Content = "Shared on social media.",
          CreatedAt = DateTime.UtcNow.AddDays(-2),
          ReportId = 1,
          ParentCommentId = 3 // Reply to CommentId = 3
      }
  // Add more Comment and Reply seed data as needed
  );
        
        ////////NOTIFICATION////////
            modelBuilder.Entity<Notification>().HasData(
                new Notification
                {
                    NotificationId = 1,
                    UserId = 1, // Assign this to an existing user Id
                    Message = "New comment on your missing person report.",
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow
                },
                new Notification
                {
                    NotificationId = 2,
                    UserId = 2, // Assign this to another existing user Id
                    Message = "Reply to your comment on a missing person report.",
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow.AddDays(-1)
                },
                new Notification
                {
                    NotificationId = 3,
                    UserId = 3, // Assign this to another existing user Id
                    Message = "New associate added to a missing person report.",
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow.AddDays(-2)
                },
                new Notification
                {
                    NotificationId = 4,
                    UserId = 4, // Assign this to another existing user Id
                    Message = "New comment on a missing person report you are following.",
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow.AddDays(-3)
                },
                new Notification
                {
                    NotificationId = 5,
                    UserId = 5, // Assign this to another existing user Id
                    Message = "New comment on a missing person report you commented on.",
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow.AddDays(-4)
                }
            // Add more Notification seed data as needed
            );
        }
    }
}
