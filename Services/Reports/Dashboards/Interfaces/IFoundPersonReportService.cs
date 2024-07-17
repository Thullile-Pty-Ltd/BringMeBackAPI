using BringMeBackAPI.Models.Reports.Dashboard;
using BringMeBackAPI.Models.Reports;

namespace BringMeBackAPI.Services.Reports.Dashboards.Interfaces
{
    public interface IFoundPersonReportService
    {
        Task<IEnumerable<FoundPersonReport>> GetAllFoundPersonReportsAsync();
        Task<FoundPersonReport> GetFoundPersonReportByIdAsync(int reportId);
        Task<IEnumerable<FoundPersonReport>> FilterFoundPersonReportsAsync(FoundPersonReportFilterParams filterParams);
        Task<object> GetFoundPersonReportsStatisticsAsync();
    }
}
