using BringMeBackAPI.Models.Reports.Dashboard;
using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Repository.Reports.Interfaces;
using BringMeBackAPI.Services.Reports.Dashboards.Interfaces;

namespace BringMeBackAPI.Services.Reports.Dashboards
{
    public class MissingItemReportService : IMissingItemReportService
    {
        private readonly IMissingItemReportRepository _missingItemReportRepository;

        public MissingItemReportService(IMissingItemReportRepository missingItemReportRepository)
        {
            _missingItemReportRepository = missingItemReportRepository;
        }

        public async Task<IEnumerable<MissingItemReport>> GetAllMissingItemReportsAsync()
        {
            return await _missingItemReportRepository.GetAllMissingItemReportsAsync();
        }

        public async Task<MissingItemReport> GetMissingItemReportByIdAsync(int reportId)
        {
            return await _missingItemReportRepository.GetMissingItemReportByIdAsync(reportId);
        }

        public async Task<IEnumerable<MissingItemReport>> FilterMissingItemReportsAsync(MissingItemReportFilterParams filterParams)
        {
            return await _missingItemReportRepository.FilterMissingItemReportsAsync(filterParams);
        }

        public async Task<object> GetMissingItemReportsStatisticsAsync()
        {
            return await _missingItemReportRepository.GetMissingItemReportsStatisticsAsync();
        }
    }
}
