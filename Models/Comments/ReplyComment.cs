namespace BringMeBackAPI.Models.Comments
{
    public class ReplyComment : BaseComment
    {
        public int? ParentCommentId { get; set; }
        public ParentComment? ParentComment { get; set; }
    }
}
