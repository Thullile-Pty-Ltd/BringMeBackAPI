using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Models.Reports.Dashboard;
using BringMeBackAPI.Repositories.Reports.Dashboards;

namespace BringMeBackAPI.Services.Reports.Dashboards
{
    public class MissingPersonReportService : IMissingPersonReportService
    {
        private readonly IMissingPersonReportRepository _missingPersonReportRepository;

        public MissingPersonReportService(IMissingPersonReportRepository missngPersonReportRepository)
        {
            _missingPersonReportRepository = missngPersonReportRepository;
        }

        public async Task<IEnumerable<MissingPersonReport>> GetAllMissingPersonReportsAsync()
        {
            return await _missingPersonReportRepository.GetAllMissingPersonReportsAsync();
        }

        public async Task<MissingPersonReport> GetMissingPersonReportByIdAsync(int reportId)
        {
            return await _missingPersonReportRepository.GetMissingPersonReportByIdAsync(reportId);
        }

        public async Task<IEnumerable<MissingPersonReport>> FilterMissingPersonReportsAsync(MissingPersonReportFilterParams MissingPersonFilterParams)
        {
            return await _missingPersonReportRepository.FilterMissingPersonReportsAsync(MissingPersonFilterParams);
        }

        public async Task<object> GetMissingPersonReportsStatisticsAsync()
        {
            // Implement logic to calculate and return statistics
            return await _missingPersonReportRepository.GetMissingPersonReportsStatisticsAsync();
        }
    }
}
