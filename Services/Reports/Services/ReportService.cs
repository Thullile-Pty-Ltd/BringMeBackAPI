using BringMeBack.Data;
using BringMeBackAPI.Models.Associates;
using BringMeBackAPI.Models.Comments;
using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Repository.Reports.Interfaces;
using BringMeBackAPI.Services.Reports.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BringMeBackAPI.Services.Reports.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _repository;

        public ReportService(IReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Report>> GetAllReports()
        {
            return await _repository.GetAllReports();
        }

        public async Task<Report> GetReportById(int id)
        {
            return await _repository.GetReportById(id);
        }

        public async Task<Report> CreateReport(Report report)
        {
            report.CreatedAt = DateTime.UtcNow;
            return await _repository.CreateReport(report);
        }

        public async Task<Report> UpdateReport(int id, Report report)
        {
            var existingReport = await _repository.GetReportById(id);
            if (existingReport == null)
            {
                throw new Exception("Report not found");
            }

            existingReport.ReportType = report.ReportType;
            existingReport.Description = report.Description;
            existingReport.IsResolved = report.IsResolved;
            existingReport.IsArchived = report.IsArchived;
            // Update other fields as necessary

            return await _repository.UpdateReport(existingReport);
        }

        public async Task<bool> ArchiveReport(int id)
        {
            var report = await _repository.GetReportById(id);
            if (report == null)
            {
                return false;
            }

            report.IsArchived = true;
            await _repository.UpdateReport(report);
            return true;
        }
    }


}
