using BringMeBack.Data;
using BringMeBackAPI.Models.Reports.Persons;
using Microsoft.EntityFrameworkCore;

namespace BringMeBackAPI.Repository.Reports.Person.Repository
{
    public class WantedPersonReportRepository : IWantedPersonReportRepository
    {
        private readonly ApplicationDbContext _context;

        public WantedPersonReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<WantedPerson> CreateWantedPersonReport(WantedPerson report)
        {
            _context.WantedPersons.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<WantedPerson> UpdateWantedPersonReport(WantedPerson report)
        {
            _context.WantedPersons.Update(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<IEnumerable<WantedPerson>> GetAllWantedPersonReportsAsync()
        {
            return await _context.WantedPersons.ToListAsync();
        }

        public async Task<WantedPerson> GetWantedPersonReportByIdAsync(int reportId)
        {
            return await _context.WantedPersons.FindAsync(reportId);
        }
    }

}
