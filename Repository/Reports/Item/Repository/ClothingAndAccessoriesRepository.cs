using BringMeBack.Data;
using BringMeBackAPI.Models.Reports.Items;
using BringMeBackAPI.Repository.Reports.Item;
using Microsoft.EntityFrameworkCore;

namespace BringMeBackAPI.Repository.Reports.Item.Repository
{
    public class ClothingAndAccessoriesRepository : IClothingAndAccessoriesRepository
    {
        private readonly ApplicationDbContext _context;

        public ClothingAndAccessoriesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ClothingAndAccessories> CreateClothingAndAccessoriesReport(ClothingAndAccessories report)
        {
            _context.ClothingAndAccessories.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<ClothingAndAccessories> UpdateClothingAndAccessoriesReport(ClothingAndAccessories report)
        {
            _context.ClothingAndAccessories.Update(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<IEnumerable<ClothingAndAccessories>> GetAllClothingAndAccessoriesReportsAsync()
        {
            return await _context.ClothingAndAccessories.ToListAsync();
        }

        public async Task<ClothingAndAccessories> GetClothingAndAccessoriesReportByIdAsync(int reportId)
        {
            return await _context.ClothingAndAccessories.FindAsync(reportId);
        }
    }

}
