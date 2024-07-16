using System.ComponentModel.DataAnnotations;

namespace BringMeBackAPI.Models.Reports
{
    public class FoundItemReport : Report
    {
        // Item Information
        [Required(ErrorMessage = "Item name is required.")]
        [MaxLength(100, ErrorMessage = "Item name cannot exceed 100 characters.")]
        public string ItemName { get; set; }

        [MaxLength(500, ErrorMessage = "Item description cannot exceed 500 characters.")]
        public string ItemDescription { get; set; }

        [MaxLength(50, ErrorMessage = "Serial number cannot exceed 50 characters.")]
        public string SerialNumber { get; set; }

        [MaxLength(200, ErrorMessage = "Unique identifiers cannot exceed 200 characters.")]
        public string UniqueIdentifiers { get; set; }

        // Current Details
        [MaxLength(100, ErrorMessage = "Found location cannot exceed 100 characters.")]
        public string FoundLocation { get; set; }

        [Required(ErrorMessage = "Found date/time is required.")]
        public DateTime FoundDateTime { get; set; }

        [MaxLength(500, ErrorMessage = "Condition of item when found cannot exceed 500 characters.")]
        public string ConditionOfItemWhenFound { get; set; }

        // Contact Information
        [MaxLength(100, ErrorMessage = "Reporting person name cannot exceed 100 characters.")]
        public string ReportingPersonName { get; set; }

        [Required(ErrorMessage = "Reporting person phone number is required.")]
        [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Invalid phone number format.")]
        public string ReportingPersonPhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string ReportingPersonEmailAddress { get; set; }

        // Additional Information
        public List<string>? RecentPhotos { get; set; }

        [MaxLength(500, ErrorMessage = "Circumstances of finding cannot exceed 500 characters.")]
        public string CircumstancesOfFinding { get; set; }

        [Url(ErrorMessage = "Invalid URL format.")]
        public string? VideoUrl { get; set; } // Added VideoUrl property
    }

}
