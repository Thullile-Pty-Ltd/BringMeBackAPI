using BringMeBack.Data;
using BringMeBackAPI.Models.Reports.Items;
using BringMeBackAPI.Repository.Reports.Item;
using Microsoft.EntityFrameworkCore;

namespace BringMeBackAPI.Repository.Reports.Item.Repository
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly ApplicationDbContext _context;

        public EquipmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Equipment> CreateEquipmentReport(Equipment report)
        {
            _context.Equipments.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Equipment> UpdateEquipmentReport(Equipment report)
        {
            _context.Equipments.Update(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<IEnumerable<Equipment>> GetAllEquipmentReportsAsync()
        {
            return await _context.Equipments.ToListAsync();
        }

        public async Task<Equipment> GetEquipmentReportByIdAsync(int reportId)
        {
            return await _context.Equipments.FindAsync(reportId);
        }
    }

}
