using BringMeBackAPI.Models.Comments;
using BringMeBackAPI.Models.Reports;

namespace BringMeBackAPI.Repository.Reports.Interfaces
{
    public interface IReportRepository
    {
        Task<IEnumerable<Report>> GetAllReports();
        Task<Report> GetReportById(int id);
        Task<Report> CreateReport(Report report);
        Task<Report> UpdateReport(Report report);
        Task<bool> ArchiveReport(int id);

        // Comment related methods
        Task<List<Comment>> GetCommentsByReportId(int reportId);
        Task<Comment> AddComment(Comment comment);
        Task<Comment> GetCommentById(int commentId);
        Task<bool> DeleteComment(int commentId);
    }
}
