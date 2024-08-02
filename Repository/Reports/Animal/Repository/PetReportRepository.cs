using BringMeBack.Data;
using BringMeBackAPI.Models.Reports.Animals;
using Microsoft.EntityFrameworkCore;

namespace BringMeBackAPI.Repository.Reports.Animal.Repository
{
    public class PetReportRepository : IPetReportRepository
    {
        private readonly ApplicationDbContext _context;

        public PetReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Pet> CreatePetReport(Pet report)
        {
            _context.Pets.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Pet> UpdatePetReport(Pet report)
        {
            _context.Pets.Update(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<IEnumerable<Pet>> GetAllPetReportsAsync()
        {
            return await _context.Pets.ToListAsync();
        }

        public async Task<Pet> GetPetReportByIdAsync(int reportId)
        {
            return await _context.Pets.FindAsync(reportId);
        }
    }
}
