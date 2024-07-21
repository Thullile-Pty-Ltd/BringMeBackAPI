using BringMeBackAPI.Models.Reports.Dashboard;
using BringMeBackAPI.Models.Reports;

namespace BringMeBackAPI.Repository.Reports.Interfaces
{
    public interface IFoundItemReportRepository
    {
        Task<FoundItemReport> CreateFoundItemReport(FoundItemReport report);
        Task<FoundItemReport> UpdateFoundItemReport(FoundItemReport report);
        Task<IEnumerable<FoundItemReport>> GetAllFoundItemReportsAsync();
        Task<FoundItemReport> GetFoundItemReportByIdAsync(int reportId);
        Task<IEnumerable<FoundItemReport>> FilterFoundItemReportsAsync(FoundItemReportFilterParams filterParams);
        Task<object> GetFoundItemReportsStatisticsAsync();
    }
}
