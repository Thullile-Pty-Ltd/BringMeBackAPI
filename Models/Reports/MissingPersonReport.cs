using System.ComponentModel.DataAnnotations;

namespace BringMeBackAPI.Models.Reports
{
    public class MissingPersonReport : BaseReport
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

        [Required(ErrorMessage = "Date of birth is required.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "ID number is required.")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "ID number must be 13 digits.")]
        public string IDNumber { get; set; }

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

        // Last Known Details
        [MaxLength(100, ErrorMessage = "Last seen location cannot exceed 100 characters.")]
        public string LastSeenLocation { get; set; }

        [Required(ErrorMessage = "Last seen date/time is required.")]
        public DateTime LastSeenDateTime { get; set; }

        [MaxLength(200, ErrorMessage = "Clothing last seen wearing cannot exceed 200 characters.")]
        public string ClothingLastSeenWearing { get; set; }

        [MaxLength(200, ErrorMessage = "Possible destinations cannot exceed 200 characters.")]
        public string PossibleDestinations { get; set; }

        // Health Information
        [MaxLength(500, ErrorMessage = "Medical conditions cannot exceed 500 characters.")]
        public string MedicalConditions { get; set; }

        [MaxLength(200, ErrorMessage = "Medications required cannot exceed 200 characters.")]
        public string MedicationsRequired { get; set; }

        [MaxLength(200, ErrorMessage = "Mental health status cannot exceed 200 characters.")]
        public string MentalHealthStatus { get; set; }

        // Contact Information
        [MaxLength(100, ErrorMessage = "Primary contact person cannot exceed 100 characters.")]
        public string PrimaryContactPerson { get; set; }

        [Required(ErrorMessage = "Contact phone number is required.")]
        [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Invalid phone number format.")]
        public string ContactPhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string ContactEmailAddress { get; set; }

        // Additional Information
        public string SocialMediaAccounts { get; set; }

        public List<string>? RecentPhotos { get; set; } // For file uploads

        [MaxLength(500, ErrorMessage = "Brief description of circumstances cannot exceed 500 characters.")]
        public string BriefDescriptionOfCircumstances { get; set; }

        [Url(ErrorMessage = "Invalid URL format.")]
        public string? VideoUrl { get; set; } // Added VideoUrl property
    }

}
