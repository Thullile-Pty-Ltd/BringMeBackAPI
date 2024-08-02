using BringMeBackAPI.Models.Reports.others;
using BringMeBackAPI.Repository.Reports.Other;
using BringMeBackAPI.Repository.Reports.Other.Repository;

namespace BringMeBackAPI.Services.Reports.Other.Service
{
    public class DangerZoneReportService : IDangerZoneReportService
    {
        private readonly IDangerZoneReportRepository _repository;

        public DangerZoneReportService(IDangerZoneReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<DangerZone> CreateDangerZoneReport(int userId, DangerZone report)
        {
            report.UserId = userId;
            return await _repository.CreateDangerZoneReport(report);
        }

        public async Task<DangerZone> UpdateDangerZoneReport(int userId, int reportId, DangerZone report)
        {
            var existingReport = await _repository.GetDangerZoneReportByIdAsync(reportId);
            if (existingReport == null || existingReport.UserId != userId)
            {
                return null; // Handle the case where the report does not exist or the user does not own it
            }

            report.ReportId = reportId;
            return await _repository.UpdateDangerZoneReport(report);
        }

        public async Task<IEnumerable<DangerZone>> GetAllDangerZoneReportsAsync()
        {
            return await _repository.GetAllDangerZoneReportsAsync();
        }

        public async Task<DangerZone> GetDangerZoneReportByIdAsync(int reportId)
        {
            return await _repository.GetDangerZoneReportByIdAsync(reportId);
        }
    }
}
