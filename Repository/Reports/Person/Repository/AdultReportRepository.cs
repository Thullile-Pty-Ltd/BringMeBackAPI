using BringMeBack.Data;
using BringMeBackAPI.Models.Reports.Persons;
using BringMeBackAPI.Repository.Reports.Person;
using Microsoft.EntityFrameworkCore;

namespace BringMeBackAPI.Repository.Reports.Person.Repository
{
    public class AdultReportRepository : IAdultReportRepository
    {
        private readonly ApplicationDbContext _context;

        public AdultReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Adult> CreateAdultReport(Adult report)
        {
            _context.Adults.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Adult> UpdateAdultReport(Adult report)
        {
            _context.Adults.Update(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<IEnumerable<Adult>> GetAllAdultReportsAsync()
        {
            return await _context.Adults.ToListAsync();
        }

        public async Task<Adult> GetAdultReportByIdAsync(int reportId)
        {
            return await _context.Adults.FindAsync(reportId);
        }
    }

}
