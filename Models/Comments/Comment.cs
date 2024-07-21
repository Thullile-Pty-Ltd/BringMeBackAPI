using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace BringMeBackAPI.Models.Comments
{
    public class Comment
    {
        public int CommentId { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public int? ParentCommentId { get; set; } // Nullable for replies
        public Comment? ParentComment { get; set; } // Navigation property for parent comment
        public List<Comment>? Replies { get; set; }
        public bool IsArchived { get; set; }

        [Required]
        public int ReportId { get; set; }
        public Report Report { get; set; }
    }

}

