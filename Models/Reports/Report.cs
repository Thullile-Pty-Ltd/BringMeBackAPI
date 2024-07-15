using BringMeBackAPI.Models.Associates;
using BringMeBackAPI.Models.Comments;
using BringMeBackAPI.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace BringMeBackAPI.Models.Reports
{
    public class Report
    {
        public int ReportId { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required(ErrorMessage = "Report type is required.")]
        [MaxLength(20, ErrorMessage = "Report type cannot exceed 20 characters.")]
        public string ReportType { get; set; } // MissingPerson, FoundPerson, MissingItem, FoundItem

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public bool IsResolved { get; set; }

        public List<Associate> Associates { get; set; }

        public List<Comment> Comments { get; set; }
    }

}
