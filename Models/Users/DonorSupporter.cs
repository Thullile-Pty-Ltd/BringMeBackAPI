using System.ComponentModel.DataAnnotations;

namespace BringMeBackAPI.Models.Users
{
    public class DonorSupporter : User
    {
        [MaxLength(100, ErrorMessage = "Donation preference cannot exceed 100 characters.")]
        public string DonationPreference { get; set; }

        public string MessageOfSupport { get; set; }

        public string PaymentInformation { get; set; }
    }

}
