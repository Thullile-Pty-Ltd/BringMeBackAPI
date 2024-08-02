using BringMeBackAPI.Models.Comments;
using BringMeBackAPI.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BringMeBackAPI.Models.Reports
{
    public abstract class BaseReport
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

       
        public string ReportType { get; set; } 

       
        public string Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public bool IsResolved { get; set; }

        [Required]
        public bool IsDiscovery { get; set; }

        public bool IsArchived { get; set; }

        // Added properties
        public DateTime? ResolvedAt { get; set; } // Date and time when the report was resolved
        public string ResolvedBy { get; set; } // Name or ID of the person who resolved the report
        public string ResolutionNotes { get; set; } // Notes on how the report was resolved
        public List<string> Attachments { get; set; } // URLs or paths to any attachments related to the report
        public List<ParentComment>? Comments { get; set; }
    }

}
