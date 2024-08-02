using BringMeBack.Data;
using BringMeBackAPI.Models.Reports.Animals;
using Microsoft.EntityFrameworkCore;

namespace BringMeBackAPI.Repository.Reports.Animal.Repository
{
    public class LivestockReportRepository : ILivestockReportRepository
    {
        private readonly ApplicationDbContext _context;

        public LivestockReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Livestock> CreateLivestockReport(Livestock report)
        {
            _context.Livestocks.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Livestock> UpdateLivestockReport(Livestock report)
        {
            _context.Livestocks.Update(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<IEnumerable<Livestock>> GetAllLivestockReportsAsync()
        {
            return await _context.Livestocks.ToListAsync();
        }

        public async Task<Livestock> GetLivestockReportByIdAsync(int reportId)
        {
            return await _context.Livestocks.FindAsync(reportId);
        }
    }
}
