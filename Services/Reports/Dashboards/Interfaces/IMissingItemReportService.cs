using BringMeBackAPI.Models.Reports.Dashboard;
using BringMeBackAPI.Models.Reports;

namespace BringMeBackAPI.Services.Reports.Dashboards.Interfaces
{
    public interface IMissingItemReportService
    {
        Task<MissingItemReport> CreateMissingItemReport(int userId, MissingItemReport report); // Only the user who creates it can update or delete
        Task<MissingItemReport> UpdateMissingItemReport(int userId, int id, MissingItemReport report); // Only the owner can update
        Task<IEnumerable<MissingItemReport>> GetAllMissingItemReportsAsync();
        Task<MissingItemReport> GetMissingItemReportByIdAsync(int reportId);
        Task<IEnumerable<MissingItemReport>> FilterMissingItemReportsAsync(MissingItemReportFilterParams filterParams);
        Task<object> GetMissingItemReportsStatisticsAsync();
    }
}
