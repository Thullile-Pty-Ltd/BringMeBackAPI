using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Models.Reports.Dashboard;

namespace BringMeBackAPI.Services.Reports.Dashboards.Interfaces
{
    public interface IMissingPersonReportService
    {
        Task<MissingPersonReport> CreateMissingPersonReport(int userId, MissingPersonReport report); // Only the user who creates it can update or delete
        Task<MissingPersonReport> UpdateMissingPersonReport(int userId, int id, MissingPersonReport report); // Only the owner can update
        Task<IEnumerable<MissingPersonReport>> GetAllMissingPersonReportsAsync();
        Task<MissingPersonReport> GetMissingPersonReportByIdAsync(int reportId);
        Task<IEnumerable<MissingPersonReport>> FilterMissingPersonReportsAsync(MissingPersonReportFilterParams filterParams);
        Task<object> GetMissingPersonReportsStatisticsAsync();
    }

}
