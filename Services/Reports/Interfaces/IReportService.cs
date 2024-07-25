using BringMeBackAPI.Models.Associates;
using BringMeBackAPI.Models.Comments;
using BringMeBackAPI.Models.Reports;
using System.Threading.Tasks;

namespace BringMeBackAPI.Services.Reports.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<Report>> GetAllReports(); // Read access for all users
        Task<Report> GetReportById(int id); // Read access for all users
        Task<Report> CreateReport(int userId, Report report); // Only the user who creates it can update or delete
        Task<Report> UpdateReport(int userId, int id, Report report); // Only the owner can update
        Task<bool> ArchiveReport(int userId, int id); // Only the owner can archive

        // Comment related methods
        Task<List<ParentComment>> GetParentCommentsByReportId(int reportId);
        Task<ParentComment> AddParentComment(int userId, int reportId, ParentComment comment);
        Task<ParentComment> GetParentCommentById(int commentId);
        Task<bool> DeleteParentComment(int userId, int commentId);

        Task<List<ReplyComment>> GetRepliesByParentCommentId(int parentCommentId);
        Task<ReplyComment> AddReplyComment(int userId, int parentCommentId, ReplyComment reply);
        Task<ReplyComment> GetReplyCommentById(int commentId);
        Task<bool> DeleteReplyComment(int userId, int commentId);
    }

}
