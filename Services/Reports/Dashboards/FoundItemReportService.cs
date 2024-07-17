using BringMeBackAPI.Models.Reports.Dashboard;
using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Repository.Reports.Interfaces;
using BringMeBackAPI.Services.Reports.Dashboards.Interfaces;

namespace BringMeBackAPI.Services.Reports.Dashboards
{
    public class FoundItemReportService : IFoundItemReportService
    {
        private readonly IFoundItemReportRepository _foundItemReportRepository;

        public FoundItemReportService(IFoundItemReportRepository foundItemReportRepository)
        {
            _foundItemReportRepository = foundItemReportRepository;
        }

        public async Task<IEnumerable<FoundItemReport>> GetAllFoundItemReportsAsync()
        {
            return await _foundItemReportRepository.GetAllFoundItemReportsAsync();
        }

        public async Task<FoundItemReport> GetFoundItemReportByIdAsync(int reportId)
        {
            return await _foundItemReportRepository.GetFoundItemReportByIdAsync(reportId);
        }

        public async Task<IEnumerable<FoundItemReport>> FilterFoundItemReportsAsync(FoundItemReportFilterParams filterParams)
        {
            return await _foundItemReportRepository.FilterFoundItemReportsAsync(filterParams);
        }

        public async Task<object> GetFoundItemReportsStatisticsAsync()
        {
            return await _foundItemReportRepository.GetFoundItemReportsStatisticsAsync();
        }
    }
}
