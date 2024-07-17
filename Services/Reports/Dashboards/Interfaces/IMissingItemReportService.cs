using BringMeBackAPI.Models.Reports.Dashboard;
using BringMeBackAPI.Models.Reports;

namespace BringMeBackAPI.Services.Reports.Dashboards.Interfaces
{
    public interface IMissingItemReportService
    {
        Task<IEnumerable<MissingItemReport>> GetAllMissingItemReportsAsync();
        Task<MissingItemReport> GetMissingItemReportByIdAsync(int reportId);
        Task<IEnumerable<MissingItemReport>> FilterMissingItemReportsAsync(MissingItemReportFilterParams filterParams);
        Task<object> GetMissingItemReportsStatisticsAsync();
    }
}
