using BringMeBack.Data;
using BringMeBackAPI.Models.Reports.Persons;
using Microsoft.EntityFrameworkCore;

namespace BringMeBackAPI.Repository.Reports.Person.Repository
{
    public class ChildReportRepository : IChildReportRepository
    {
        private readonly ApplicationDbContext _context;

        public ChildReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Child> CreateChildReport(Child report)
        {
            _context.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Child> UpdateChildReport(Child report)
        {
            _context.Update(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<IEnumerable<Child>> GetAllChildReportsAsync()
        {
            return await _context.Set<Child>().ToListAsync();
        }

        public async Task<Child> GetChildReportByIdAsync(int reportId)
        {
            return await _context.Set<Child>().FindAsync(reportId);
        }
    }

}
