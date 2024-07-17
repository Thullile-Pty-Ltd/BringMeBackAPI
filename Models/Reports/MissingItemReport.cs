using System.ComponentModel.DataAnnotations;

namespace BringMeBackAPI.Models.Reports
{
    public class MissingItemReport : Report
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

        // Last Known Details
        [MaxLength(100, ErrorMessage = "Last known location cannot exceed 100 characters.")]
        public string LastKnownLocation { get; set; }

        [Required(ErrorMessage = "Last seen date/time is required.")]
        public DateTime LastSeenDateTime { get; set; }

        [MaxLength(500, ErrorMessage = "Circumstances of loss cannot exceed 500 characters.")]
        public string CircumstancesOfLoss { get; set; }

        // Contact Information
        [MaxLength(100, ErrorMessage = "Owner name cannot exceed 100 characters.")]
        public string OwnerName { get; set; }

        [Required(ErrorMessage = "Owner phone number is required.")]
        [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Invalid phone number format.")]
        public string OwnerPhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string OwnerEmailAddress { get; set; }

        // Additional Information
        public List<string>? RecentPhotos { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Estimated value must be greater than zero.")]
        public decimal? EstimatedValue { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Reward offered must be greater than zero.")]
        public decimal? RewardOffered { get; set; }

        [Url(ErrorMessage = "Invalid URL format.")]
        public string VideoUrl { get; set; } // Added VideoUrl property
    }

}
