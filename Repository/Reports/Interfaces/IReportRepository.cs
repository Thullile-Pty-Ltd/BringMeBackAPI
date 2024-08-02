using BringMeBackAPI.Models.Comments;
using BringMeBackAPI.Models.Reports;

namespace BringMeBackAPI.Repository.Reports.Interfaces
{
    public interface IReportRepository
    {
        Task<IEnumerable<BaseReport>> GetAllReports();
        Task<BaseReport> GetReportById(int id);
        Task<BaseReport> CreateReport(BaseReport report);
        Task<BaseReport> UpdateReport(BaseReport report);
        Task<bool> ArchiveReport(int id);

        // Comment related methods
        Task<List<ParentComment>> GetParentCommentsByReportId(int reportId);
        Task<ParentComment> AddParentComment(ParentComment comment);
        Task<ParentComment> GetParentCommentById(int commentId);
        Task<bool> DeleteParentComment(int commentId);

        Task<List<ReplyComment>> GetRepliesByParentCommentId(int parentCommentId);
        Task<ReplyComment> AddReplyComment(ReplyComment reply);
        Task<ReplyComment> GetReplyCommentById(int commentId);
        Task<bool> DeleteReplyComment(int commentId);
    }
}
