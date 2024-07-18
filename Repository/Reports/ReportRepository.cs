using BringMeBack.Data;
using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Repository.Reports.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BringMeBackAPI.Repository.Reports
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Report>> GetAllReports()
        {
            return await _context.Reports.Where(r => !r.IsArchived).ToListAsync();
        }

        public async Task<Report> GetReportById(int id)
        {
            return await _context.Reports.FindAsync(id);
        }

        public async Task<Report> CreateReport(Report report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Report> UpdateReport(Report report)
        {
            _context.Reports.Update(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<bool> ArchiveReport(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null) return false;
            report.IsArchived = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
