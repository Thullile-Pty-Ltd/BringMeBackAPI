using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BringMeBackAPI.Models.Comments
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }

        public int? ReportId { get; set; }
        public Report? Report { get; set; }

        public int? ParentCommentId { get; set; } // For replies, null for main comments
        public Comment? ParentComment { get; set; }
        public List<Comment> Replies { get; set; } = new List<Comment>();

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

