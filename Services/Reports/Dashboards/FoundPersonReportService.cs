using BringMeBackAPI.Models.Reports.Dashboard;
using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Repository.Reports.Interfaces;
using BringMeBackAPI.Services.Reports.Dashboards.Interfaces;

namespace BringMeBackAPI.Services.Reports.Dashboards
{
    public class FoundPersonReportService : IFoundPersonReportService
    {
        private readonly IFoundPersonReportRepository _foundPersonReportRepository;

        public FoundPersonReportService(IFoundPersonReportRepository foundPersonReportRepository)
        {
            _foundPersonReportRepository = foundPersonReportRepository;
        }

        public async Task<IEnumerable<FoundPersonReport>> GetAllFoundPersonReportsAsync()
        {
            return await _foundPersonReportRepository.GetAllFoundPersonReportsAsync();
        }

        public async Task<FoundPersonReport> GetFoundPersonReportByIdAsync(int reportId)
        {
            return await _foundPersonReportRepository.GetFoundPersonReportByIdAsync(reportId);
        }

        public async Task<IEnumerable<FoundPersonReport>> FilterFoundPersonReportsAsync(FoundPersonReportFilterParams filterParams)
        {
            return await _foundPersonReportRepository.FilterFoundPersonReportsAsync(filterParams);
        }

        public async Task<object> GetFoundPersonReportsStatisticsAsync()
        {
            return await _foundPersonReportRepository.GetFoundPersonReportsStatisticsAsync();
        }
    }
}
