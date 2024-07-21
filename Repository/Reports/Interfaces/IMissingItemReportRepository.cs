using BringMeBackAPI.Models.Reports.Dashboard;
using BringMeBackAPI.Models.Reports;

namespace BringMeBackAPI.Repository.Reports.Interfaces
{
    public interface IMissingItemReportRepository
    {
        Task<MissingItemReport> CreateMissingItemReport(MissingItemReport report);
        Task<MissingItemReport> UpdateMissingItemReport(MissingItemReport report);
        Task<IEnumerable<MissingItemReport>> GetAllMissingItemReportsAsync();
        Task<MissingItemReport> GetMissingItemReportByIdAsync(int reportId);
        Task<IEnumerable<MissingItemReport>> FilterMissingItemReportsAsync(MissingItemReportFilterParams filterParams);
        Task<object> GetMissingItemReportsStatisticsAsync();
    }
}
