using BringMeBackAPI.Models.Associates;
using BringMeBackAPI.Models.Comments;
using BringMeBackAPI.Models.Reports;

namespace BringMeBackAPI.Services.Reports.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<Report>> GetAllReports();
        Task<Report> GetReportById(int id);
        Task<Report> CreateReport(Report report);
        Task<Report> UpdateReport(int id, Report report);
        Task<bool> ArchiveReport(int id);
    }

}
