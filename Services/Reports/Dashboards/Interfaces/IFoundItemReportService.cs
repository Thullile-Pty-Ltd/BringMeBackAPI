using BringMeBackAPI.Models.Reports.Dashboard;
using BringMeBackAPI.Models.Reports;

namespace BringMeBackAPI.Services.Reports.Dashboards.Interfaces
{
    public interface IFoundItemReportService
    {
        Task<FoundItemReport> CreateFoundItemReport(int userId, FoundItemReport report); // Only the user who creates it can update or delete
        Task<FoundItemReport> UpdateFoundItemReport(int userId, int id, FoundItemReport report); // Only the owner can update
        Task<IEnumerable<FoundItemReport>> GetAllFoundItemReportsAsync();
        Task<FoundItemReport> GetFoundItemReportByIdAsync(int reportId);
        Task<IEnumerable<FoundItemReport>> FilterFoundItemReportsAsync(FoundItemReportFilterParams filterParams);
        Task<object> GetFoundItemReportsStatisticsAsync();
    }
}
