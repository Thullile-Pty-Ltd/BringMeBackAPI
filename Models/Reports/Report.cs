using BringMeBackAPI.Models.Associates;
using BringMeBackAPI.Models.Comments;
using BringMeBackAPI.Models.Reports.DTOs;
using BringMeBackAPI.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BringMeBackAPI.Models.Reports
{
    public class Report
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReportId { get; set; }
                
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserLocation { get; set; }
        public UserRole UserRole { get; set; }
       

        [Required(ErrorMessage = "Report type is required.")]
        [MaxLength(20, ErrorMessage = "Report type cannot exceed 20 characters.")]
        public string ReportType { get; set; } // MissingPerson, FoundPerson, MissingItem, FoundItem

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public bool IsResolved { get; set; }

        public bool IsArchived { get; set; }

        public List<Associate>? Associates { get; set; }

        public List<Comment>? Comments { get; set; }
    }

}
