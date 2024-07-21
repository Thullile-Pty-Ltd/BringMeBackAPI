using BringMeBackAPI.Models.Reports.Dashboard;
using BringMeBackAPI.Models.Reports;

namespace BringMeBackAPI.Repository.Reports.Interfaces
{
    public interface IFoundPersonReportRepository
    {
        Task<FoundPersonReport> CreateFoundPersonReport(FoundPersonReport report);
        Task<FoundPersonReport> UpdateFoundPersonReport(FoundPersonReport report);
        Task<IEnumerable<FoundPersonReport>> GetAllFoundPersonReportsAsync();
        Task<FoundPersonReport> GetFoundPersonReportByIdAsync(int reportId);
        Task<IEnumerable<FoundPersonReport>> FilterFoundPersonReportsAsync(FoundPersonReportFilterParams filterParams);
        Task<object> GetFoundPersonReportsStatisticsAsync();
    }
}
