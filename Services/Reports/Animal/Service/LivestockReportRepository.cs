using BringMeBackAPI.Models.Reports.Animals;
using BringMeBackAPI.Repository.Reports.Animal;

namespace BringMeBackAPI.Services.Reports.Animal.Service
{
    public class LivestockReportService : ILivestockReportService
    {
        private readonly ILivestockReportRepository _repository;

        public LivestockReportService(ILivestockReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<Livestock> CreateLivestockReport(int userId, Livestock report)
        {
            report.UserId = userId;
            return await _repository.CreateLivestockReport(report);
        }

        public async Task<Livestock> UpdateLivestockReport(int userId, int reportId, Livestock report)
        {
            var existingReport = await _repository.GetLivestockReportByIdAsync(reportId);
            if (existingReport == null || existingReport.UserId != userId)
            {
                return null; // Handle the case where the report does not exist or the user does not own it
            }

            report.ReportId = reportId;
            return await _repository.UpdateLivestockReport(report);
        }

        public async Task<IEnumerable<Livestock>> GetAllLivestockReportsAsync()
        {
            return await _repository.GetAllLivestockReportsAsync();
        }

        public async Task<Livestock> GetLivestockReportByIdAsync(int reportId)
        {
            return await _repository.GetLivestockReportByIdAsync(reportId);
        }
    }

}
