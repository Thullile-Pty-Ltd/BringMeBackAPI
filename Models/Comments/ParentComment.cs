namespace BringMeBackAPI.Models.Comments
{
    public class ParentComment : BaseComment
    {
        public List<ReplyComment> Replies { get; set; } = new List<ReplyComment>();
    }
}
