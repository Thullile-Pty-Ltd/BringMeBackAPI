using BringMeBack.Data;
using BringMeBackAPI.Models.Reports.Animals;
using BringMeBackAPI.Models.Reports.others;
using BringMeBackAPI.Repository.Reports.Animal;
using Microsoft.EntityFrameworkCore;

namespace BringMeBackAPI.Repository.Reports.Other.Repository
{
    public class AnnouncementReportRepository : IAnnouncementReportRepository
    {
        private readonly ApplicationDbContext _context;

        public AnnouncementReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Announcement> CreateAnnouncementReport(Announcement report)
        {
            _context.Announcements.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Announcement> UpdateAnnouncementReport(Announcement report)
        {
            _context.Announcements.Update(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<IEnumerable<Announcement>> GetAllAnnouncementReportsAsync()
        {
            return await _context.Announcements.ToListAsync();
        }

        public async Task<Announcement> GetAnnouncementReportByIdAsync(int reportId)
        {
            return await _context.Announcements.FindAsync(reportId);
        }
    }
}
