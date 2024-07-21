using BringMeBack.Data;
using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Repository.Reports.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BringMeBackAPI.Repository.Reports
{
    using BringMeBackAPI.Models.Reports;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Report>> GetAllReports()
        {
            // Return all reports. Adjust as needed based on your requirements.
            return await _context.Reports
                .Include(r => r.Associates)
                .Include(r => r.Comments)
                .ToListAsync();
        }

        public async Task<Report> GetReportById(int id)
        {
            return await _context.Reports
                .Include(r => r.Associates)
                .Include(r => r.Comments)
                .FirstOrDefaultAsync(r => r.ReportId == id);
        }

        public async Task<Report> CreateReport(Report report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }
       
        public async Task<FoundPersonReport> CreateFoundPersonReport(FoundPersonReport report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }
        public async Task<MissingItemReport> CreateMissingItemReport(MissingItemReport report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }
        public async Task<FoundItemReport> CreateFoundItemReport(FoundItemReport report)
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
            if (report == null)
            {
                return false;
            }

            report.IsArchived = true;
            _context.Reports.Update(report);

            if (report.Comments != null)
            {
                foreach (var comment in report.Comments)
                {
                    comment.IsArchived = true;
                    _context.Comments.Update(comment);
                }
            }           

            await _context.SaveChangesAsync();
            return true;
        }
    }

}
