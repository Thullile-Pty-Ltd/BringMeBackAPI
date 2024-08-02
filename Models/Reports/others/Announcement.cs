using BringMeBackAPI.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BringMeBackAPI.Models.Reports.others
{
    public class Announcement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReportId { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public UserRole Role { get; set; }

        public string Title { get; set; } // Title of the announcement
        public string Content { get; set; } // Detailed content or message
        public DateTime PostedDate { get; set; } // Date when the announcement was posted
        public string Author { get; set; } // Person or entity posting the announcement

        // Additional fields
        public string Category { get; set; } // Category of the announcement (e.g., General, Emergency)
        public bool IsUrgent { get; set; } // Whether the announcement is urgent
        public DateTime ExpiryDate { get; set; } // Expiry date of the announcement
        public List<string> Attachments { get; set; } // URLs or paths to attachments
    }

}
