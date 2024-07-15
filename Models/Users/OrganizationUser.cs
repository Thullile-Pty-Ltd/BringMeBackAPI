using System.ComponentModel.DataAnnotations;

namespace BringMeBackAPI.Models.Users
{
    public class OrganizationUser : User
    {
        [Required(ErrorMessage = "Organization name is required.")]
        [MaxLength(200, ErrorMessage = "Organization name cannot exceed 200 characters.")]
        public string OrganizationName { get; set; }

        [MaxLength(50, ErrorMessage = "Organization type cannot exceed 50 characters.")]
        public string OrganizationType { get; set; }

        [MaxLength(50, ErrorMessage = "Registration number cannot exceed 50 characters.")]
        public string RegistrationNumber { get; set; }

        [MaxLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string Address { get; set; }

        [MaxLength(100, ErrorMessage = "Contact person name cannot exceed 100 characters.")]
        public string ContactPerson { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string ContactPhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string ContactEmail { get; set; } = string.Empty;
    }
}
