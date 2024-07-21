using BringMeBackAPI.Models.Reports.Dashboard;
using BringMeBackAPI.Models.Reports;

namespace BringMeBackAPI.Services.Reports.Dashboards.Interfaces
{
    public interface IFoundPersonReportService
    {
        Task<FoundPersonReport> CreateFoundPersonReport(int userId, FoundPersonReport report); // Only the user who creates it can update or delete
        Task<FoundPersonReport> UpdateFoundPersonReport(int userId, int id, FoundPersonReport report); // Only the owner can update
        Task<IEnumerable<FoundPersonReport>> GetAllFoundPersonReportsAsync();
        Task<FoundPersonReport> GetFoundPersonReportByIdAsync(int reportId);
        Task<IEnumerable<FoundPersonReport>> FilterFoundPersonReportsAsync(FoundPersonReportFilterParams filterParams);
        Task<object> GetFoundPersonReportsStatisticsAsync();
    }
}
