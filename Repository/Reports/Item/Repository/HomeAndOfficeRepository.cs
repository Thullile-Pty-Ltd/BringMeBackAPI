using BringMeBack.Data;
using BringMeBackAPI.Models.Reports.Items;
using BringMeBackAPI.Repository.Reports.Item;
using Microsoft.EntityFrameworkCore;

namespace BringMeBackAPI.Repository.Reports.Item.Repository
{
    public class HomeAndOfficeRepository : IHomeAndOfficeRepository
    {
        private readonly ApplicationDbContext _context;

        public HomeAndOfficeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<HomeAndOffice> CreateHomeAndOfficeReport(HomeAndOffice report)
        {
            _context.HomeAndOffices.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<HomeAndOffice> UpdateHomeAndOfficeReport(HomeAndOffice report)
        {
            _context.HomeAndOffices.Update(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<IEnumerable<HomeAndOffice>> GetAllHomeAndOfficeReportsAsync()
        {
            return await _context.HomeAndOffices.ToListAsync();
        }

        public async Task<HomeAndOffice> GetHomeAndOfficeReportByIdAsync(int reportId)
        {
            return await _context.HomeAndOffices.FindAsync(reportId);
        }
    }

}
