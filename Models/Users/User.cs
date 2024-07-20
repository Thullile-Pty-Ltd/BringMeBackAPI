using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Models.Comments;
using BringMeBackAPI.Models.Payments;

namespace BringMeBackAPI.Models.Users
{
    public enum UserRole
    {
        General,
        Organization,
        CommunityMember,
        FamilyMember,
        PublicAuthority,
        Volunteer,
        DonorSupporter
    }

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(100, ErrorMessage = "Description cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "UserEmail address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        [MaxLength(100, ErrorMessage = "Password cannot exceed 100 characters.")]
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Phone(ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }

        [MaxLength(200, ErrorMessage = "UserLocation cannot exceed 200 characters.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "UserRole is required.")]
        public UserRole Role { get; set; }

        public List<Report>? Reports { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<Donation>? Donations { get; set; }
    }

}
