using BringMeBack.Data;
using BringMeBackAPI.Models.Reports.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace BringMeBackAPI.Repository.Reports.Vehicle.Repository
{
    public class BusReportRepository : IBusReportRepository
    {
        private readonly ApplicationDbContext _context;

        public BusReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Bus> CreateBusReport(Bus report)
        {
            _context.Buses.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Bus> UpdateBusReport(Bus report)
        {
            _context.Buses.Update(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<IEnumerable<Bus>> GetAllBusReportsAsync()
        {
            return await _context.Buses.ToListAsync();
        }

        public async Task<Bus> GetBusReportByIdAsync(int reportId)
        {
            return await _context.Buses.FindAsync(reportId);
        }
    }

}
