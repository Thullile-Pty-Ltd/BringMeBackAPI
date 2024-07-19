using BringMeBack.Data;
using BringMeBackAPI.Models.Associates;
using BringMeBackAPI.Models.Comments;
using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Repository.Reports.Interfaces;
using BringMeBackAPI.Services.Reports.Interfaces;
using BringMeBackAPI.Services.Users.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BringMeBackAPI.Services.Reports.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IUserService _userService;

        public ReportService(IReportRepository reportRepository, IUserService userService)
        {
            _reportRepository = reportRepository;
            _userService = userService;
        }

        public async Task<IEnumerable<Report>> GetAllReports()
        {
            return await _reportRepository.GetAllReports();
        }

        public async Task<Report> GetReportById(int id)
        {
            return await _reportRepository.GetReportById(id);
        }

        public async Task<Report> CreateReport(int userId, Report report)
        {
            // Ensure the UserId in the report matches the logged-in user
            report.UserId = userId;
            return await _reportRepository.CreateReport(report);
        }

        public async Task<Report> UpdateReport(int userId, int id, Report report)
        {
            var existingReport = await _reportRepository.GetReportById(id);
            if (existingReport == null)
            {
                throw new Exception("Report not found.");
            }

            // Ensure the logged-in user is the owner of the report
            if (existingReport.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to update this report.");
            }

            // Update the report details
            existingReport.Description = report.Description;
            existingReport.ReportType = report.ReportType;
            existingReport.IsResolved = report.IsResolved;
            existingReport.IsArchived = report.IsArchived;

            return await _reportRepository.UpdateReport(existingReport);
        }

        public async Task<bool> ArchiveReport(int userId, int id)
        {
            var existingReport = await _reportRepository.GetReportById(id);
            if (existingReport == null)
            {
                throw new Exception("Report not found.");
            }

            // Ensure the logged-in user is the owner of the report
            if (existingReport.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to archive this report.");
            }

            return await _reportRepository.ArchiveReport(id);
        }
    }

}
