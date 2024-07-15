using System.ComponentModel.DataAnnotations;

namespace BringMeBackAPI.Models.Users
{
    public class PublicAuthority : User
    {
        [MaxLength(100, ErrorMessage = "Position or agency name cannot exceed 100 characters.")]
        public string PositionOrAgency { get; set; }

        public bool Authorization { get; set; }

        [MaxLength(200, ErrorMessage = "Access credentials cannot exceed 200 characters.")]
        public string AccessCredentials { get; set; } = string.Empty;
    }
}
