using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Models.Reports.Dashboard;

namespace BringMeBackAPI.Services.Reports.Dashboards
{
    public interface IMissingPersonReportService
    {
        Task<IEnumerable<MissingPersonReport>> GetAllMissingPersonReportsAsync();
        Task<MissingPersonReport> GetMissingPersonReportByIdAsync(int reportId);
        Task<IEnumerable<MissingPersonReport>> FilterMissingPersonReportsAsync(MissingPersonReportFilterParams filterParams);
        Task<object> GetMissingPersonReportsStatisticsAsync();
    }
    
}
