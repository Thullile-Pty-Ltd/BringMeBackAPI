using BringMeBackAPI.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BringMeBackAPI.Models.Reports.others
{
    public class DangerZone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReportId { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string PhoneNumber { get; set; }
        public UserRole Role { get; set; }
        public string Title { get; set; } // Title of the danger zone alert
        public string Content { get; set; } // Detailed content or message
        public DateTime PostedDate { get; set; } // Date when the alert was posted
        public string Author { get; set; } // Person or entity posting the alert

        // Additional fields
        public string Location { get; set; } // Location of the danger zone
        public string Severity { get; set; } // Severity of the danger (e.g., High, Medium, Low)
        public List<string> Precautions { get; set; } // List of precautions to take
        public DateTime ExpiryDate { get; set; } // Expiry date of the alert
        public List<string> Attachments { get; set; } // URLs or paths to attachments
    }

}
