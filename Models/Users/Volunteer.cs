using System.ComponentModel.DataAnnotations;

namespace BringMeBackAPI.Models.Users
{
    public class Volunteer : User
    {
        [MaxLength(200, ErrorMessage = "Volunteer experience cannot exceed 200 characters.")]
        public string VolunteerExperience { get; set; }

        [MaxLength(100, ErrorMessage = "Availability description cannot exceed 100 characters.")]
        public string Availability { get; set; }

        [MaxLength(100, ErrorMessage = "Interest area cannot exceed 100 characters.")]
        public string InterestArea { get; set; }
    }

}
