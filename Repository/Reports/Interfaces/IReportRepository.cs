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
    }
}
