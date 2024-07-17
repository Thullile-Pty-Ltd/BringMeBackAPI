using BringMeBackAPI.Models.Reports.Dashboard;
using BringMeBackAPI.Models.Reports;

namespace BringMeBackAPI.Services.Reports.Dashboards.Interfaces
{
    public interface IFoundItemReportService
    {
        Task<IEnumerable<FoundItemReport>> GetAllFoundItemReportsAsync();
        Task<FoundItemReport> GetFoundItemReportByIdAsync(int reportId);
        Task<IEnumerable<FoundItemReport>> FilterFoundItemReportsAsync(FoundItemReportFilterParams filterParams);
        Task<object> GetFoundItemReportsStatisticsAsync();
    }
}
