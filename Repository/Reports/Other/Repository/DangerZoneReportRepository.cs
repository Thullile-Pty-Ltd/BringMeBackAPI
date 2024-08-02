using BringMeBack.Data;
using BringMeBackAPI.Models.Reports.Animals;
using BringMeBackAPI.Models.Reports.others;
using BringMeBackAPI.Repository.Reports.Animal;
using Microsoft.EntityFrameworkCore;

namespace BringMeBackAPI.Repository.Reports.Other.Repository
{
    public class DangerZoneReportRepository : IDangerZoneReportRepository
    {
        private readonly ApplicationDbContext _context;

        public DangerZoneReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DangerZone> CreateDangerZoneReport(DangerZone report)
        {
            _context.DangerZones.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<DangerZone> UpdateDangerZoneReport(DangerZone report)
        {
            _context.DangerZones.Update(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<IEnumerable<DangerZone>> GetAllDangerZoneReportsAsync()
        {
            return await _context.DangerZones.ToListAsync();
        }

        public async Task<DangerZone> GetDangerZoneReportByIdAsync(int reportId)
        {
            return await _context.DangerZones.FindAsync(reportId);
        }
    }
}
