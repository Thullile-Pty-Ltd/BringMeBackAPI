using BringMeBack.Data;
using BringMeBackAPI.Models.Reports.Persons;
using BringMeBackAPI.Repository.Reports.Person;
using Microsoft.EntityFrameworkCore;

namespace BringMeBackAPI.Repository.Reports.Person.Repository
{
    public class SeniorCitizenReportRepository : ISeniorCitizenReportRepository
    {
        private readonly ApplicationDbContext _context;

        public SeniorCitizenReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SeniorCitizen> CreateSeniorCitizenReport(SeniorCitizen report)
        {
            _context.SeniorCitizens.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<SeniorCitizen> UpdateSeniorCitizenReport(SeniorCitizen report)
        {
            _context.SeniorCitizens.Update(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<IEnumerable<SeniorCitizen>> GetAllSeniorCitizenReportsAsync()
        {
            return await _context.SeniorCitizens.ToListAsync();
        }

        public async Task<SeniorCitizen> GetSeniorCitizenReportByIdAsync(int reportId)
        {
            return await _context.SeniorCitizens.FindAsync(reportId);
        }
    }

}
