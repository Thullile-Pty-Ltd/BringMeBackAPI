using BringMeBackAPI.Models.Reports;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BringMeBackAPI.Models.Comments
{
    public class BaseComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }

        public int? ReportId { get; set; }
        public BaseReport? Report { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Comment cannot exceed 500 characters.")]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
