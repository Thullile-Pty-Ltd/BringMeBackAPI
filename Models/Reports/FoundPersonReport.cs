using System.ComponentModel.DataAnnotations;

namespace BringMeBackAPI.Models.Reports
{
    public class FoundPersonReport : Report
    {
        // Personal Information
        [Required(ErrorMessage = "Full name is required.")]
        [MaxLength(100, ErrorMessage = "Full name cannot exceed 100 characters.")]
        public string FullName { get; set; }

        [MaxLength(50, ErrorMessage = "Nickname cannot exceed 50 characters.")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [MaxLength(10, ErrorMessage = "Gender cannot exceed 10 characters.")]
        public string Gender { get; set; }

        [Range(0, 150, ErrorMessage = "Estimated age must be between 0 and 150.")]
        public int? EstimatedAge { get; set; }

        [Required(ErrorMessage = "Nationality is required.")]
        [MaxLength(50, ErrorMessage = "Nationality cannot exceed 50 characters.")]
        public string Nationality { get; set; }

        // Physical Description
        [MaxLength(20, ErrorMessage = "Height cannot exceed 20 characters.")]
        public string Height { get; set; }

        [MaxLength(20, ErrorMessage = "Weight cannot exceed 20 characters.")]
        public string Weight { get; set; }

        [MaxLength(20, ErrorMessage = "Eye color cannot exceed 20 characters.")]
        public string EyeColor { get; set; }

        [MaxLength(20, ErrorMessage = "Hair color cannot exceed 20 characters.")]
        public string HairColor { get; set; }

        [MaxLength(200, ErrorMessage = "Distinguishing marks/features cannot exceed 200 characters.")]
        public string DistinguishingMarksOrFeatures { get; set; }

        // Current Details
        [MaxLength(100, ErrorMessage = "Found location cannot exceed 100 characters.")]
        public string FoundLocation { get; set; }

        [Required(ErrorMessage = "Found date/time is required.")]
        public DateTime FoundDateTime { get; set; }

        [MaxLength(200, ErrorMessage = "Clothing at time of finding cannot exceed 200 characters.")]
        public string ClothingAtTimeOfFinding { get; set; }

        [MaxLength(200, ErrorMessage = "Condition when found cannot exceed 200 characters.")]
        public string ConditionWhenFound { get; set; }

        // Health Information
        [MaxLength(500, ErrorMessage = "Observed medical conditions cannot exceed 500 characters.")]
        public string ObservedMedicalConditions { get; set; }

        [MaxLength(200, ErrorMessage = "Observed medications cannot exceed 200 characters.")]
        public string ObservedMedications { get; set; }

        [MaxLength(200, ErrorMessage = "Observed mental health status cannot exceed 200 characters.")]
        public string ObservedMentalHealthStatus { get; set; }

        public List<string>? RecentPhotos { get; set; } // For file uploads

        [Url(ErrorMessage = "Invalid URL format.")]
        public string? VideoUrl { get; set; } // Added VideoUrl property

    }
}
