using BringMeBackAPI.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace BringMeBackAPI.Models.Verification
{
    public class Verification
    {
        public int VerificationId { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required(ErrorMessage = "Verification code is required.")]
        [MaxLength(10, ErrorMessage = "Verification code cannot exceed 10 characters.")]
        public string VerificationCode { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public bool IsUsed { get; set; }
    }

}
