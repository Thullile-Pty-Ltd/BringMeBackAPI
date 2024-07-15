using BringMeBackAPI.Models.Associates;
using BringMeBackAPI.Models.Comments;
using BringMeBackAPI.Models.Reports;

namespace BringMeBackAPI.Services.Reports.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<Report>> GetAllReportsAsync();
        Task<Report> GetReportByIdAsync(int reportId);
        Task<Report> CreateReportAsync(Report report);
        Task<Report> UpdateReportAsync(Report report);
        Task<bool> DeleteReportAsync(int reportId);

        Task<IEnumerable<PersonReport>> GetAllPersonReportsAsync();
        Task<IEnumerable<ItemReport>> GetAllItemReportsAsync();
        Task<IEnumerable<FoundPersonReport>> GetAllFoundPersonReportsAsync();
        Task<IEnumerable<FoundItemReport>> GetAllFoundItemReportsAsync();

        //Task<bool> AddReportToUserTrackingAsync(int userId, int reportId);
    }

    public interface IAssociateService
    {
        Task<IEnumerable<Associate>> GetAssociatesByReportIdAsync(int reportId);
        Task AddAssociateAsync(int reportId, Associate associate);
        Task RemoveAssociateAsync(int reportId, int associateId);
    }

    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetCommentsByReportIdAsync(int reportId);
        Task AddCommentAsync(int reportId, Comment comment);
        Task RemoveCommentAsync(int reportId, int commentId);
    }

}
