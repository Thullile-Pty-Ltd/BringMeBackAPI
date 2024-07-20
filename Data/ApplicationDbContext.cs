using Microsoft.EntityFrameworkCore;
using BringMeBackAPI.Models.Users;
using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Models.Notifications;
using BringMeBackAPI.Models.Verification;
using BringMeBackAPI.Models.Associates;
using BringMeBackAPI.Models.Comments;
using BringMeBackAPI.Models.Reports.Dashboard;
using BringMeBackAPI.Models.Reports.DTOs;

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
        public DbSet<MissingItemReportFilterParams> MissingItemReportFilterParamss { get; set; }
        public DbSet<FoundPersonReportFilterParams> FoundPersonReportFilterParamss { get; set; }
        public DbSet<FoundPersonReportFilterParams> FoundItemReportFilterParamss { get; set; }

        //Associates
        public DbSet<Associate> Associates { get; set; }

        //comments
         public DbSet<Comment> Comments { get; set; }

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
        }
    }
}
