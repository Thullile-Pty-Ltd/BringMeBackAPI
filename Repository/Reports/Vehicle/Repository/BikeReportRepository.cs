using BringMeBack.Data;
using BringMeBackAPI.Models.Reports.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace BringMeBackAPI.Repository.Reports.Vehicle.Repository
{
    public class BikeReportRepository : IBikeReportRepository
    {
        private readonly ApplicationDbContext _context;

        public BikeReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Bike> CreateBikeReport(Bike report)
        {
            _context.Bikes.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Bike> UpdateBikeReport(Bike report)
        {
            _context.Bikes.Update(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<IEnumerable<Bike>> GetAllBikeReportsAsync()
        {
            return await _context.Bikes.ToListAsync();
        }

        public async Task<Bike> GetBikeReportByIdAsync(int reportId)
        {
            return await _context.Bikes.FindAsync(reportId);
        }
    }

}
