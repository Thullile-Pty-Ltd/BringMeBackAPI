using BringMeBack.Data;
using BringMeBackAPI.Models.Reports.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace BringMeBackAPI.Repository.Reports.Vehicle.Repository
{
    public class HeavyDutyMachineryReportRepository : IHeavyDutyMachineryReportRepository
    {
        private readonly ApplicationDbContext _context;

        public HeavyDutyMachineryReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<HeavyDutyMachinery> CreateHeavyDutyMachineryReport(HeavyDutyMachinery report)
        {
            _context.HeavyDutyMachineries.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<HeavyDutyMachinery> UpdateHeavyDutyMachineryReport(HeavyDutyMachinery report)
        {
            _context.HeavyDutyMachineries.Update(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<IEnumerable<HeavyDutyMachinery>> GetAllHeavyDutyMachineryReportsAsync()
        {
            return await _context.HeavyDutyMachineries.ToListAsync();
        }

        public async Task<HeavyDutyMachinery> GetHeavyDutyMachineryReportByIdAsync(int reportId)
        {
            return await _context.HeavyDutyMachineries.FindAsync(reportId);
        }
    }

}
