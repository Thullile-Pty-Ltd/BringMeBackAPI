using BringMeBack.Data;
using BringMeBackAPI.Models.Reports.Animals;
using Microsoft.EntityFrameworkCore;

namespace BringMeBackAPI.Repository.Reports.Animal.Repository
{
    public class WildReportRepository : IWildReportRepository
    {
        private readonly ApplicationDbContext _context;

        public WildReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Wild> CreateWildReport(Wild report)
        {
            _context.Wilds.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Wild> UpdateWildReport(Wild report)
        {
            _context.Wilds.Update(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<IEnumerable<Wild>> GetAllWildReportsAsync()
        {
            return await _context.Wilds.ToListAsync();
        }

        public async Task<Wild> GetWildReportByIdAsync(int reportId)
        {
            return await _context.Wilds.FindAsync(reportId);
        }
    }
}
