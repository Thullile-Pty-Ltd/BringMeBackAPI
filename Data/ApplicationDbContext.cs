using Microsoft.EntityFrameworkCore;
using BringMeBackAPI.Models.Users;
using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Models.Notifications;
using BringMeBackAPI.Models.Verification;
using BringMeBackAPI.Models.Comments;
using BringMeBackAPI.Models.Reports.Dashboard;
using BringMeBackAPI.Models.Reports.Animals;
using BringMeBackAPI.Models.Reports.Items;
using BringMeBackAPI.Models.Reports.others;
using BringMeBackAPI.Models.Reports.Persons;
using BringMeBackAPI.Models.Reports.Vehicles;

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
        public DbSet<BaseReport> Reports { get; set; }
        public DbSet<MissingPersonReport> MissingPersonReports { get; set; }
        public DbSet<MissingItemReport> MissingItemReports { get; set; }
        public DbSet<FoundPersonReport> FoundPersonReports { get; set; }
        public DbSet<FoundItemReport> FoundItemReports { get; set; }



        // New DbSets
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<ClothingAndAccessories> ClothingAndAccessories { get; set; }
        public DbSet<DangerZone> DangerZones { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Adult> Adults { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<SeniorCitizen> SeniorCitizens { get; set; }
        public DbSet<WantedPerson> WantedPersons { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Livestock> Livestocks { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Wild> Wilds { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<HeavyDutyMachinery> HeavyDutyMachineries { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<HomeAndOffice> HomeAndOffices { get; set; }
        public DbSet<Device> Devices { get; set; }      

        //comments
        // New DbSets for comments
        public DbSet<ParentComment> ParentComments { get; set; }
        public DbSet<ReplyComment> ReplyComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
            // Configure keyless entity types
            modelBuilder.Entity<MissingPersonReportFilterParams>().HasNoKey();
            modelBuilder.Entity<MissingItemReportFilterParams>().HasNoKey();
            modelBuilder.Entity<FoundPersonReportFilterParams>().HasNoKey();
            modelBuilder.Entity<FoundItemReportFilterParams>().HasNoKey();

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
            modelBuilder.Entity<BaseReport>()
                .ToTable("Reports");

            modelBuilder.Entity<MissingItemReport>()
                .ToTable("MissingItemReports");

            modelBuilder.Entity<MissingPersonReport>()
                .ToTable("MissingPersonReports");

            modelBuilder.Entity<FoundPersonReport>()
                .ToTable("FoundPersonReports");

            modelBuilder.Entity<FoundItemReport>()
                .ToTable("FoundItemReport");           

            // Configure the ParentComment entity
            modelBuilder.Entity<ParentComment>()
                .HasKey(pc => pc.CommentId);

            modelBuilder.Entity<ParentComment>()
                .HasMany(pc => pc.Replies)
                .WithOne(r => r.ParentComment)
                .HasForeignKey(r => r.ParentCommentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure the ReplyComment entity
            modelBuilder.Entity<ReplyComment>()
                .HasKey(rc => rc.CommentId);
        }
    }
}
