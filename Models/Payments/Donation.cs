using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace BringMeBackAPI.Models.Payments
{
    public class Donation
    {
        public int DonationId { get; set; }              

        [Required]
        public int OrganizationUserId { get; set; }
        public OrganizationUser Organization { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime DonatedAt { get; set; }

        [Required(ErrorMessage = "Donation type is required.")]
        [MaxLength(50, ErrorMessage = "Donation type cannot exceed 50 characters.")]
        public string DonationType { get; set; } // e.g., Food, Clothing, Shelter
    }


}

