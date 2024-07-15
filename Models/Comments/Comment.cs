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
        public List<Comment> Replies { get; set; }
    }

}

