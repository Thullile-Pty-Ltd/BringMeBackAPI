using BringMeBackAPI.Models.Reports.Animals;
using BringMeBackAPI.Repository.Reports.Animal;

namespace BringMeBackAPI.Services.Reports.Animal.Service
{
    public class WildReportService : IWildReportService
    {
        private readonly IWildReportRepository _repository;

        public WildReportService(IWildReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<Wild> CreateWildReport(int userId, Wild report)
        {
            report.UserId = userId;
            return await _repository.CreateWildReport(report);
        }

        public async Task<Wild> UpdateWildReport(int userId, int reportId, Wild report)
        {
            var existingReport = await _repository.GetWildReportByIdAsync(reportId);
            if (existingReport == null || existingReport.UserId != userId)
            {
                return null; // Handle the case where the report does not exist or the user does not own it
            }

            report.ReportId = reportId;
            return await _repository.UpdateWildReport(report);
        }

        public async Task<IEnumerable<Wild>> GetAllWildReportsAsync()
        {
            return await _repository.GetAllWildReportsAsync();
        }

        public async Task<Wild> GetWildReportByIdAsync(int reportId)
        {
            return await _repository.GetWildReportByIdAsync(reportId);
        }
    }

}
