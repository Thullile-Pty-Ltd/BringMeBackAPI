using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Models.Reports.Dashboard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BringMeBackAPI.Repository.Reports.Interfaces
{
    public interface IMissingPersonReportRepository
    {
        Task<MissingPersonReport> CreateMissingPersonReport(MissingPersonReport rReport);
        Task<MissingPersonReport> UpdateMissingPersonReport(MissingPersonReport report);
        Task<IEnumerable<MissingPersonReport>> GetAllMissingPersonReportsAsync();
        Task<MissingPersonReport> GetMissingPersonReportByIdAsync(int reportId);
        Task<IEnumerable<MissingPersonReport>> FilterMissingPersonReportsAsync(MissingPersonReportFilterParams filterParams);
        Task<object> GetMissingPersonReportsStatisticsAsync();
    }
}
