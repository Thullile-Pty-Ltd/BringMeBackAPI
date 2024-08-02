using BringMeBack.Data;
using BringMeBackAPI.Models.Reports.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace BringMeBackAPI.Repository.Reports.Vehicle.Repository
{
    public class CarReportRepository : ICarReportRepository
    {
        private readonly ApplicationDbContext _context;

        public CarReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Car> CreateCarReport(Car report)
        {
            _context.Cars.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Car> UpdateCarReport(Car report)
        {
            _context.Cars.Update(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<IEnumerable<Car>> GetAllCarReportsAsync()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car> GetCarReportByIdAsync(int reportId)
        {
            return await _context.Cars.FindAsync(reportId);
        }
    }

}
