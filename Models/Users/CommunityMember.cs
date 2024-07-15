using System.ComponentModel.DataAnnotations;

namespace BringMeBackAPI.Models.Users
{
    public class CommunityMember : User
    {
        [MaxLength(100, ErrorMessage = "Community role cannot exceed 100 characters.")]
        public string CommunityRole { get; set; }

        [MaxLength(100, ErrorMessage = "Community affiliation cannot exceed 100 characters.")]
        public string CommunityAffiliation { get; set; }

        public bool Verification { get; set; }
    }

}
