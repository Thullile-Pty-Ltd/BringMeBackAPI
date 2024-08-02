using BringMeBackAPI.Models.Reports.Animals;
using BringMeBackAPI.Models.Reports.others;
using BringMeBackAPI.Repository.Reports.Animal;
using BringMeBackAPI.Repository.Reports.Other;
using BringMeBackAPI.Services.Reports.Animal;

namespace BringMeBackAPI.Services.Reports.Other.Service
{
    public class AnnouncementReportService : IAnnouncementReportService
    {
        private readonly IAnnouncementReportRepository _repository;

        public AnnouncementReportService(IAnnouncementReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<Announcement> CreateAnnouncementReport(int userId, Announcement report)
        {
            report.UserId = userId;
            return await _repository.CreateAnnouncementReport(report);
        }

        public async Task<Announcement> UpdateAnnouncementReport(int userId, int reportId, Announcement report)
        {
            var existingReport = await _repository.GetAnnouncementReportByIdAsync(reportId);
            if (existingReport == null || existingReport.UserId != userId)
            {
                return null; // Handle the case where the report does not exist or the user does not own it
            }

            report.ReportId = reportId;
            return await _repository.UpdateAnnouncementReport(report);
        }

        public async Task<IEnumerable<Announcement>> GetAllAnnouncementReportsAsync()
        {
            return await _repository.GetAllAnnouncementReportsAsync();
        }

        public async Task<Announcement> GetAnnouncementReportByIdAsync(int reportId)
        {
            return await _repository.GetAnnouncementReportByIdAsync(reportId);
        }
    }
}
