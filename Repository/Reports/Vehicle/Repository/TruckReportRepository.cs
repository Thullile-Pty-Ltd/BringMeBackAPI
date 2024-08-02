using BringMeBack.Data;
using BringMeBackAPI.Models.Reports.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace BringMeBackAPI.Repository.Reports.Vehicle.Repository
{
    public class TruckReportRepository : ITruckReportRepository
    {
        private readonly ApplicationDbContext _context;

        public TruckReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Truck> CreateTruckReport(Truck report)
        {
            _context.Trucks.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Truck> UpdateTruckReport(Truck report)
        {
            _context.Trucks.Update(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<IEnumerable<Truck>> GetAllTruckReportsAsync()
        {
            return await _context.Trucks.ToListAsync();
        }

        public async Task<Truck> GetTruckReportByIdAsync(int reportId)
        {
            return await _context.Trucks.FindAsync(reportId);
        }
    }

}
