using BringMeBackAPI.Models.Reports.Dashboard;
using BringMeBackAPI.Models.Reports;

namespace BringMeBackAPI.Repository.Reports.Interfaces
{
    public interface IFoundPersonReportRepository
    {
        Task<IEnumerable<FoundPersonReport>> GetAllFoundPersonReportsAsync();
        Task<FoundPersonReport> GetFoundPersonReportByIdAsync(int reportId);
        Task<IEnumerable<FoundPersonReport>> FilterFoundPersonReportsAsync(FoundPersonReportFilterParams filterParams);
        Task<object> GetFoundPersonReportsStatisticsAsync();
    }
}
